#!/usr/bin/env bash
set -euo pipefail

# ==================================================
# PROOK / GreenYellowSite Deployment Script
# ==================================================

APP_NAME="GreenYellowSite"
PROJECT_FILE="./GreenYellowSite.csproj"

# Server configuration
DEPLOY_DIR="/home/prook/www"
SERVICE_NAME="prook.service"

# Local folders
PUBLISH_DIR="./publish"
BACKUP_ROOT="./backups"

TIMESTAMP=$(date +"%Y%m%d_%H%M%S")
BACKUP_DIR="$BACKUP_ROOT/$TIMESTAMP"

echo "=================================================="
echo " Deploying $APP_NAME"
echo " Started: $(date)"
echo "=================================================="

rollback() {
    echo ""
    echo "❌ Deploy failed!"
    echo "Attempting rollback..."

    if [ -d "$BACKUP_DIR" ]; then
        sudo systemctl stop "$SERVICE_NAME"

        sudo rsync -a --delete "$BACKUP_DIR"/ "$DEPLOY_DIR"/

        sudo chown -R prook:prook "$DEPLOY_DIR"

        sudo systemctl start "$SERVICE_NAME"

        echo "✅ Rollback completed."
    else
        echo "⚠ No backup found. Rollback skipped."
    fi

    exit 1
}

trap rollback ERR

echo ""
echo "1/10 Pull latest changes..."
git pull

echo ""
echo "2/10 Install npm packages..."
if [ -f package-lock.json ]; then
    npm ci
else
    npm install
fi

echo ""
echo "3/10 Build Tailwind CSS..."
npm run build:css:prod

echo ""
echo "4/10 Restore NuGet packages..."
dotnet restore "$PROJECT_FILE"

echo ""
echo "5/10 Build project..."
dotnet build "$PROJECT_FILE" \
    --configuration Release \
    --no-restore

echo ""
echo "6/10 Publish project..."
rm -rf "$PUBLISH_DIR"

dotnet publish "$PROJECT_FILE" \
    --configuration Release \
    --output "$PUBLISH_DIR" \
    --no-build

echo ""
echo "7/10 Create backup..."

mkdir -p "$BACKUP_ROOT"

if [ -d "$DEPLOY_DIR" ]; then
    mkdir -p "$BACKUP_DIR"
    sudo rsync -a "$DEPLOY_DIR"/ "$BACKUP_DIR"/
    echo "Backup created:"
    echo "$BACKUP_DIR"
else
    echo "Deploy directory not found. Skipping backup."
fi

echo ""
echo "8/10 Stop service..."
sudo systemctl stop "$SERVICE_NAME"

echo ""
echo "9/10 Deploy new version..."

sudo mkdir -p "$DEPLOY_DIR"

sudo rsync -a --delete \
    "$PUBLISH_DIR"/ \
    "$DEPLOY_DIR"/

sudo chown -R prook:prook "$DEPLOY_DIR"

echo ""
echo "10/10 Start service..."
sudo systemctl start "$SERVICE_NAME"

echo ""
echo "Checking service status..."

if systemctl is-active --quiet "$SERVICE_NAME"; then
    echo "✅ Service is running."
else
    echo "❌ Service failed to start."
    rollback
fi

trap - ERR

echo ""
echo "=================================================="
echo " Deployment completed successfully!"
echo "=================================================="

echo ""
sudo systemctl --no-pager --full status "$SERVICE_NAME"