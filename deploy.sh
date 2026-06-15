#!/usr/bin/env bash
set -e

echo "Pull latest changes..."
git pull

echo "Install npm dependencies..."
npm install

echo "Build CSS..."
npm run build:css:prod

echo "Restore project..."
dotnet restore ./GreenYellowSite.csproj

echo "Build project..."
dotnet build ./GreenYellowSite.csproj --configuration Release

echo "Publish project..."
rm -rf ./publish
dotnet publish ./GreenYellowSite.csproj --configuration Release --output ./publish

echo "Done!"