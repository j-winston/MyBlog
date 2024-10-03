# ASP.NET Core Blog Application

A clean, web-based blog application developed using **ASP.NET Core MVC**, **Entity Framework Core**, **Identity for authentication**, **Azure SQL Database** for data storage, and **Docker** for containerization and deployment. 
I made this project to demonstrate modern web development practices including CI/CD with Azure DevOps and containerization using Docker.

## Features

- **User Authentication**: Powered by ASP.NET Core Identity for secure user registration, login, and password management.
- **Blog Post Management**: Users can create, edit, and delete their blog posts.
- **Image Upload**: Users can upload images for their blog posts.
- **Database**: Azure SQL Database is used for storing user data and blog content(Local MS Sql Server used for Development purposes)
- **Containerized Deployment**: The application is containerized with Docker and deployed to Azure Web Apps.

## Technologies Used
 
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **ASP.NET Core Identity**
- **Azure SQL Database (Production db)**
- **Docker**
- **Bootstrap**
- **Azure Web Apps**
- **MS SQL Server(Development Db)**

## Prerequisites

- .NET SDK 8.0+
- Docker Desktop
- Azure account (for deployment)
- Azure SQL Database

## Getting Started

## 1. Clone the Repository
```bash
git clone https://github.com/yourusername/my-blog-app.git
cd my-blog-app

```
## 2. Setup Environment Variables

Before running the application, you need to set up the required environment variables. Depending on your environment (Development or Production), you may need to configure specific settings in `appsettings.Development.json` or `appsettings.Production.json`.

### ASP.NET Core Environment

- For **Development** environment:
  ```bash
  export ASPNETCORE_ENVIRONMENT=Development

- For ** Production** environment:
```bash
export ASPNETCORE_ENVIRONMENT=Production
```

## 3. Apply Migrations
```bash
dotnet ef database update
```

## 4. Run the Application
### Local Development:
```bash
dotnet run
```
Navigate to `http://localhost:5005` on your browser. (This may change depending on your configuration)

### Docker Setup 
```bash
docker-compose up --build
```

## 5. Deployment
- Set up CI/CD with Azure DevOps
- Push Docker imaged to Docker Hub(or wherever you choose)
- Deploy to Azure Web App

## Screenshots

### Home Page
![Home Page](/MyBlog/Screenshots/home.png)


### Post Details
![Post Details](/MyBlog/Screenshots/post-details.png)


### Admin Panel
![Admin Panel](/MyBlog/Screenshots/admin-panel.png)


### Login
![Login](/MyBlog/Screenshots/login.png)


### Update Post
![Update Post](/MyBlog/Screenshots/update-post.png)



