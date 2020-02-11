FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Activity.Services.ActivityWebApi/Activity.Services.ActivityWebApi.csproj", "Activity.Services.ActivityWebApi/"]
RUN dotnet restore "Activity.Services.ActivityWebApi/Activity.Services.ActivityWebApi.csproj"
COPY . .
WORKDIR "/src/Activity.Services.ActivityWebApi"
RUN dotnet build "Activity.Services.ActivityWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Activity.Services.ActivityWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Activity.Services.ActivityWebApi.dll"]