#!/bin/bash
set -e

echo "Pull latest changes..."
git pull

echo "Restore dependencies..."
dotnet restore

echo "Build project..."
dotnet build --configuration Release

echo "Run tests..."
dotnet test --configuration Release

echo "Publish project..."
dotnet publish GreenYellowSite.csproj --configuration Release --output ./publish

echo "Done!"
