#!/bin/bash
set -e

echo "Pull latest changes..."
git pull

echo "Install npm dependencies..."
npm install

echo "Build CSS..."
npm run build:css:prod

echo "Restore dependencies..."
dotnet restore

echo "Build project..."
dotnet build --configuration Release

echo "Run tests..."
dotnet test --configuration Release

echo "Publish project..."
rm -rf ./publish
dotnet publish GreenYellowSite.csproj --configuration Release --output ./publish

echo "Done!"
