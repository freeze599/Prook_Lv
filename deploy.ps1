Write-Host "Pull latest changes..."
git pull

Write-Host "Restore dependencies..."
dotnet restore

Write-Host "Build project..."
dotnet build --configuration Release

Write-Host "Run tests..."
dotnet test --configuration Release

Write-Host "Publish project..."
dotnet publish .\GreenYellowSite.csproj --configuration Release --output ./publish

Write-Host "Done!"