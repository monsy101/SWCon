# intro
made kareem yasser mahmoud rashid
this is a project for the Software Construction course on Deploying a FullStack Microservices with CI/CD


# provided instructios
Step 1: Version Control & Project Setup
1. Create a GitHub Repository:
   - Log in to GitHub and create a new repository for your project. Name it according to the application’s purpose (e.g., AdventureWorksMicroservices).
   - Add a descriptive README.md to the repository with sections for project objectives, technology stack, setup instructions, and architecture overview.
2. Clone the Repository Locally:
   - Open a terminal on your local machine and clone the repository:
git clone <your-repo-url>
   - Navigate into the cloned directory:
cd AdventureWorksMicroservices
3. Initialize Git for Version Control:
git init
   - Create .gitignore to exclude unnecessary files from version control (e.g., bin/, obj/):
bin/
obj/
*.log
*.db
4. Commit Best Practices:
   - Regularly commit changes as you develop, using meaningful commit messages:
git add .
git commit -m 'Initialize project with README and .gitignore'
Step 2: Database Integration
1. Set Up MS SQL Server:
   - Download and install MS SQL Server.
   - Open SQL Server Management Studio (SSMS) or a command-line interface.
2. Load Adventure Works Database:
   - Download the Adventure Works database and attach it in SSMS.
   - Use appropriate SQL commands to attach the database:
USE [master];
CREATE DATABASE AdventureWorks
ON (FILENAME = 'C:\Path\To\AdventureWorks.mdf')
FOR ATTACH;
3. REST API Project in Visual Studio:
   - Open Visual Studio and create a new ASP.NET Core Web API project.
   - Configure the connection string in appsettings.json for the AdventureWorks database:
"ConnectionStrings": {
   "DefaultConnection": "Server=localhost;Database=AdventureWorks;User Id=<username>;Password=<password>;"
}
4. Create Controllers and CRUD Operations:
   - Implement CRUD operations for selected tables using Entity Framework Core.
   - Scaffold the database context and create API endpoints:
dotnet ef dbcontext scaffold "Server=localhost;Database=AdventureWorks;User Id=<username>;Password=<password>;" Microsoft.EntityFrameworkCore.SqlServer
Step 3: Microservices Architecture with Docker
1. Set Up Docker:
   - Download and install Docker Desktop. Ensure Docker is running.
2. Create Dockerfiles:
   - Dockerfile for REST API: In the project folder, create a Dockerfile:
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
COPY . .
ENTRYPOINT ["dotnet", "YourApiProjectName.dll"]
   - Dockerfile for MS SQL Server: Pull the MS SQL Server image if using Docker:
docker pull mcr.microsoft.com/mssql/server
3. Set Up Docker Compose:
   - Create a docker-compose.yml file to manage the API and database containers:
version: '3.8'
services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server
    environment:
      SA_PASSWORD: "YourStrong@Passw0rd"
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
  api:
    build: .
    depends_on:
      - sqlserver
    environment:
      ConnectionStrings__DefaultConnection: "Server=sqlserver;Database=AdventureWorks;User Id=sa;Password=YourStrong@Passw0rd;"
    ports:
      - "8080:80"
4. Build and Run Containers:
docker-compose up --build
Step 4: Set Up CI/CD Pipeline with Jenkins
1. Install Jenkins:
   - Install Jenkins and access it at http://localhost:8080.
2. Integrate GitHub with Jenkins:
   - Configure GitHub webhooks to trigger Jenkins builds on each commit.
3. Jenkins Pipeline Setup:
   - Create a Jenkinsfile with stages: Build, Docker Build, and Deploy to Staging:
pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        script {
          sh 'dotnet build YourApiProjectName.sln'
        }
      }
    }
    stage('Docker Build') {
      steps {
        script {
          sh 'docker build -t your-api-image-name .'
        }
      }
    }
    stage('Deploy to Staging') {
      steps {
        script {
          sh 'docker-compose up --build -d'
        }
      }
    }
  }
}
Step 5: Documentation
1. Document Architecture and Setup:
   - Provide detailed explanations for each component.
   - Include network or architecture diagrams.
2. API Documentation:
   - Use Swagger for API documentation and provide interactive API specs.
   - Include setup instructions in the README.md.
### PLEASE NOTE THAT THERE ARE PROBLEMS THE ROSE FROM THESE STEPS


# Version control with git
Q: Why are we using github?
A: GitHub is a platform for hosting and collaborating on code using Git version control. It's great because it helps developers track changes, work on projects together, and access tools for documentation, CI/CD, and project management—all in one place

overall setting up git and github was extremely easy because any new IDE will have version control support through git and other stuff
for me i used VS Code  where i can signin with my github account and it will both create a repo for my work and commit and push all the work
![1_4SJhCY05XrGBsAkQ8bJPYA](https://github.com/user-attachments/assets/a142ac4f-b516-42f3-ba1c-6de8a06d8a43)


# Database integration

For data base we use MS SQL Server. which is a relational database management system developed by Microsoft. It's great for handling large-scale data, ensuring security, and offering robust tools for analytics, data integration, and reporting, making it ideal for enterprise applications.

specificaly we are using a premade data base called adventure works database. which is a sample database provided by Microsoft for learning and testing SQL Server features. It represents a fictional company, Adventure Works Cycles, and includes realistic data for scenarios like sales, manufacturing, and human resources, making it ideal for practice and demonstrations.

after install both ssms and adventure works database
to connect it to our project in vistual studio we first need to make sure we have the needed packages wich can be downloaded with the following commands:

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet tool install --global dotnet-ef

those adds the sqlserver and tools packages to out enviroment then install global tools to out project

after that we use this command to hook out database to the project:

dotnet ef dbcontext scaffold "Server=LAPTOP-J52AD55K\SQLEXPRESS;Database=AdventureWorks2022;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer

### NOTE THAT THE SERVER WILL CHANGE DEPENDING ON YOUR PC AND DATABASE 

# Microservices Architecture with Docker
after installing docker which is a platform for creating, deploying, and managing applications in lightweight, portable containers. It's great because it ensures consistency across environments, simplifies deployment, and optimizes resource use, making it a key tool for modern software development and DevOps.

we create 2 files in our project [dockerfile , docker-compose.yml]
the docker file will have this data:
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
COPY . .
ENTRYPOINT ["dotnet", "YourApiProjectName.dll"]

and the docker-compose.yml will have:
version: '3.8'
services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server
    environment:
      SA_PASSWORD: "YourStrong@Passw0rd"
      ACCEPT_EULA: "Y"
    ports:
      - "1434:1433"
  api:
    build: .
    depends_on:
      - sqlserver
    environment:
      ConnectionStrings__DefaultConnection: "Server=sqlserver;Database=AdventureWorks;User Id=sa;Password=YourStrong@Passw0rd;"
    ports:
      - "8081:8080"

### NOTE THAT YOU HAVE TO DOUBLE CHECK THE PORTS USED OR IT WILL NOT WORK

after setting both files we then run the command: 
docker-compose up --build

this will create and mage file in out docker desktop software that we will have to use later

![Architecture-of-Docker](https://github.com/user-attachments/assets/d324d52b-a15b-42b6-8b2d-69f0c5726587)

# Setting up ci/cd Pipeline with Jenkins
Jenkins is an open-source automation server used to build, test, and deploy software projects. It is widely used for continuous integration (CI) and continuous delivery/deployment (CD) pipelines, enabling developers to automate repetitive tasks, detect errors early, and streamline the software development lifecycle.

after installing jenkins, going to the hosted webpage you will be able to access jenkins and create a new project
selecting pipeline and adding you github information to it will make it connected to your github repo 
after that commit all changes to git hub you can then you can start building for your new pipeline 
first create a Jenkinsfile in your project with the data:
pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        script {
          bat 'dotnet build SWCon.sln'
        }
      }
    }
    stage('Docker Build') {
      steps {
        script {
          bat 'docker build -t swcon-api .'
        }
      }
    }
    stage('Deploy to Staging') {
      steps {
        script {
          bat 'docker-compose up --build -d'//test
        }
      }
    }
  }
}
