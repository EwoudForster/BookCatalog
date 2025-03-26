# Step 1: Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory
WORKDIR /src

# Copy the .csproj files to restore dependencies first
COPY ["BookCatalog/BookCatalog.csproj", "BookCatalog/"]
COPY ["BookCatalog.DataLayer/BookCatalog.DataLayer.csproj", "BookCatalog.DataLayer/"]
COPY ["BookCatalog.ServiceDefaults/BookCatalog.ServiceDefaults.csproj", "BookCatalog.ServiceDefaults/"]

# Restore the dependencies
RUN dotnet restore "BookCatalog/BookCatalog.csproj"

# Copy all the other files into the container
COPY . .

# Build the project
WORKDIR "/src/BookCatalog"
RUN dotnet build "BookCatalog.csproj" -c Release -o /app/build

# Step 2: Publish the application to be ready for deployment
RUN dotnet publish "BookCatalog.csproj" -c Release -o /app/publish

# Step 3: Prepare the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

# Set the working directory in the runtime container
WORKDIR /app
EXPOSE 80

# Copy the published files from the build container
COPY --from=build /app/publish .

# Step 4: Set the entry point for the application
ENTRYPOINT ["dotnet", "BookCatalog.dll"]
