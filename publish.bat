dotnet build -c Release
dotnet pack ./src/Cike.Modularization/Cike.Modularization.csproj -c Release -o .\publish\Cike.Modularization
dotnet pack ./src/Cike.Modularization.ApplicationInitialization/Cike.Modularization.ApplicationInitialization.csproj -c Release -o .\publish\Cike.Modularization.ApplicationInitialization
dotnet pack ./src/Cike.Modularization.DependencyInjection/Cike.Modularization.DependencyInjection.csproj -c Release -o .\publish\Cike.Modularization.DependencyInjection
dotnet pack ./src/Cike.Utils/Cike.Utils.csproj -c Release -o .\publish\Cike.Utils
