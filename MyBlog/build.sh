#!/bin/bash 

# Clean previous builds 
echo "Cleaning project...."
dotnet clean 

# Build the project in Release mode"
echo "Building project..."
dotnet publish --configuration Release 

echo "Running tests..."
dotnet test --configuration Release 

# Pull down any docker images
docker-compose down  
echo "Pulling down any images..."

# Clean just in case 
echo "Removing stopped containers..."
docker system prune -f

# Clear docker cache
echo "Clearing docker cache..."
docker builder prune -f

# Compose new images 
echo "Building images...."
docker-compose build 

# Tag images 
echo "Tagging docker images..."
docker tag myblog-myblog jameswinston/myblog:v8
echo "Images tagged."

# Push images to docker hub
echo "Pushing images to docker hub..."
docker push jameswinston/myblog:v8
echo "Images pushed"

