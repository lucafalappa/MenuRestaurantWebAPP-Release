#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MenuRestaurantWebAPP.MVC/MenuRestaurantWebAPP.MVC.csproj", "MenuRestaurantWebAPP.MVC/"]
COPY ["MenuRestaurantWebAPP.ContextServices/MenuRestaurantWebAPP.ContextServices.csproj", "MenuRestaurantWebAPP.ContextServices/"]
COPY ["MenuRestaurantWebAPP.Contexts/MenuRestaurantWebAPP.Contexts.csproj", "MenuRestaurantWebAPP.Contexts/"]
COPY ["MenuRestaurantWebAPP.Models/MenuRestaurantWebAPP.Models.csproj", "MenuRestaurantWebAPP.Models/"]
RUN dotnet restore "./MenuRestaurantWebAPP.MVC/MenuRestaurantWebAPP.MVC.csproj"
COPY . .
WORKDIR "/src/MenuRestaurantWebAPP.MVC"
RUN dotnet build "./MenuRestaurantWebAPP.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MenuRestaurantWebAPP.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MenuRestaurantWebAPP.MVC.dll"]