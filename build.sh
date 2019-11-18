set -euo pipefail

dotnet build ./src/PublicApiGenerator.sln --configuration Release /nologo
dotnet test ./src/PublicApiGenerator.sln --configuration Release --no-build --verbosity=normal /nologo
dotnet publish ./src/PublicApiGenerator/PublicApiGenerator.csproj --configuration Release --no-build /nologo
dotnet pack ./src/PublicApiGenerator/PublicApiGenerator.csproj --configuration Release --no-build /nologo
dotnet publish ./src/PublicApiGenerator.Tool/PublicApiGenerator.Tool.csproj --configuration Release --no-build /nologo
dotnet pack ./src/PublicApiGenerator.Tool/PublicApiGenerator.Tool.csproj --configuration Release --no-build /nologo
