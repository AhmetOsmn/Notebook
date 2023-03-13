FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Templates.csproj", "Templates/"]
RUN dotnet restore "Templates/Templates.csproj"

COPY . Templates/.
RUN dotnet publish -c Release -o /app Templates/Templates.csproj 

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS publish
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Templates.dll"]