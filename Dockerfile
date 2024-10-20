# Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build-env

# Set the working directory
WORKDIR /App

# Copy everything to the container
COPY . ./

# Restore the dependencies
RUN dotnet restore

# Build and publish the application in Release mode
RUN dotnet publish -c Release -o out

# Build the runtime image using the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:6c4df091e4e531bb93bdbfe7e7f0998e7ced344f54426b7e874116a3dc3233ff

# Set the working directory for the runtime image
WORKDIR /App

# Copy the published output from the build stage
COPY --from=build-env /App/out .

# Ensure the public directory is copied as well
COPY --from=build-env /App/public ./public

# Expose the ports that the application will use
EXPOSE 80
EXPOSE 443

# Set the entry point for the application
ENTRYPOINT ["dotnet", "cheeseBackend.dll"]
