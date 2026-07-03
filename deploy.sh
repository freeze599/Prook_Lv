#!/usr/bin/env bash
set -euo pipefail

# ==================================================
# PROOK / GreenYellowSite Deployment Script
# Bez sudo tiesībām
# ==================================================

APP_NAME="GreenYellowSite"
PROJECT_FILE="./GreenYellowSite.csproj"

DEPLOY_DIR="/home/prook/www"

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
        rsync -a --delete "$BACKUP_DIR"/ "$DEPLOY_DIR"/
        echo "✅ Rollback completed."
        echo "⚠ Service restart must be done separately."
    else
        echo "⚠ No backup found. Rollback skipped."
    fi

    exit 1
}

trap rollback ERR

echo ""
echo "1/8 Pull latest changes..."
git pull

echo ""
echo "2/8 Install npm packages..."
if [ -f package-lock.json ]; then
    npm ci
else
    npm install
fi

echo ""
echo "3/8 Build Tailwind CSS..."
npm run build:css:prod

echo ""
echo "4/8 Restore NuGet packages..."
dotnet restore "$PROJECT_FILE"

echo ""
echo "5/8 Build project..."
dotnet build "$PROJECT_FILE" \
    --configuration Release \
    --no-restore

echo ""
echo "6/8 Publish project..."
rm -rf "$PUBLISH_DIR"

dotnet publish "$PROJECT_FILE" \
    --configuration Release \
    --output "$PUBLISH_DIR" \
    --no-build

echo ""
echo "7/8 Create backup..."

mkdir -p "$BACKUP_ROOT"

if [ -d "$DEPLOY_DIR" ]; then
    mkdir -p "$BACKUP_DIR"
    rsync -a "$DEPLOY_DIR"/ "$BACKUP_DIR"/
    echo "Backup created:"
    echo "$BACKUP_DIR"
else
    echo "Deploy directory not found. Skipping backup."
fi

echo ""
echo "8/8 Deploy new version..."

mkdir -p "$DEPLOY_DIR"

rsync -a --delete \
    "$PUBLISH_DIR"/ \
    "$DEPLOY_DIR"/

trap - ERR

echo ""
echo "=================================================="
echo " Deployment files copied successfully!"
echo "=================================================="

echo ""
echo "⚠ IMPORTANT:"
echo "Service restart must be done separately by deployer/admin:"
echo "systemctl restart prook.service"
echo ""