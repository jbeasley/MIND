FROM microsoft/aspnetcore-build:2.0.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore SigmaCloudManager/*.csproj
RUN dotnet publish SigmaCloudManager/*.csproj -c Release -o out

FROM microsoft/aspnetcore:2.0.0
WORKDIR /app
COPY --from=build /app/SigmaCloudManager/out ./
ENTRYPOINT ["dotnet", "Mind.dll"]

