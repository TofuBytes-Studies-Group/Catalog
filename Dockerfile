# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution and project files
COPY *.sln ./
COPY Catalog.API/*.csproj Catalog.API/
COPY Catalog.Domain/*.csproj Catalog.Domain/
COPY Catalog.Infrastructure/*.csproj Catalog.Infrastructure/
COPY Catalog.DTO/*.csproj Catalog.DTO/
COPY Catalog.UnitTests/*.csproj Catalog.UnitTests/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the application source code
COPY . ./

# Build and publish the application
RUN dotnet publish Catalog.API/Catalog.API.csproj -c Release -o /app/out

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the build output from the previous stage
COPY --from=build /app/out .

# Expose the port your application listens on
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Catalog.API.dll"]