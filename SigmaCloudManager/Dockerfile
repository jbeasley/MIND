FROM microsoft/aspnetcore-build:2.0.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build app
COPY . ./
RUN dotnet publish -c Release -o out


FROM microsoft/aspnetcore:2.0.0
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "Mind.dll"]
