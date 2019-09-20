set -euo pipefail

dotnet build ./src/ApiApprover.sln --configuration Release
dotnet test ./src/ApiApprover.sln --configuration Release --no-build --verbosity=normal
dotnet publish ./src/PublicApiGenerator/PublicApiGenerator.csproj --configuration Release --no-build /nologo
dotnet pack ./src/PublicApiGenerator/PublicApiGenerator.csproj --configuration Release --no-build /nologo
dotnet publish ./src/PublicApiGenerator.Tool/PublicApiGenerator.Tool.csproj --configuration Release --no-build /nologo
dotnet pack ./src/PublicApiGenerator.Tool/PublicApiGenerator.Tool.csproj --configuration Release --no-build /nologo