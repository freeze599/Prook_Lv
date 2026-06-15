#!/usr/bin/env bash
set -e

echo "Pull latest changes..."
git pull

echo "Restore project..."
dotnet restore ./GreenYellowSite.csproj

echo "Build project..."
dotnet build ./GreenYellowSite.csproj --configuration Release

echo "Publish project..."
dotnet publish ./GreenYellowSite.csproj --configuration Release --output ./publish

echo "Done!"