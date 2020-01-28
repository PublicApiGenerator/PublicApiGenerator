@echo Off
setlocal
cd "%~dp0"

dotnet build ./src/PublicApiGenerator.sln --configuration Release /nologo  || goto :error
dotnet test ./src/PublicApiGenerator.sln --configuration Release --no-build --verbosity=normal /nologo  || goto :error
dotnet publish ./src/PublicApiGenerator/PublicApiGenerator.csproj --configuration Release --no-build /nologo || goto :error
dotnet pack ./src/PublicApiGenerator/PublicApiGenerator.csproj --configuration Release --no-build /nologo  || goto :error
dotnet publish ./src/PublicApiGenerator.Tool/PublicApiGenerator.Tool.csproj --configuration Release --no-build /nologo  || goto :error
dotnet pack ./src/PublicApiGenerator.Tool/PublicApiGenerator.Tool.csproj --configuration Release --no-build /nologo || goto :error

goto :EOF
:error
exit /b %errorlevel%
