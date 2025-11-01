# ToDoApp

[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Docker](https://img.shields.io/badge/Docker-Container-blue.svg)](https://hub.docker.com/r/yurii0liinyk/todoapp.api)
[![Swagger](https://img.shields.io/badge/Swagger-API%20Documentation-green.svg)](https://swagger.io/)
## Table of Contents
- [About the Project](#about-the-project)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the Application](#running-the-application)
  - [Running with Docker](#running-with-docker)
  - [Running with Docker Compose](#running-with-docker-compose)
- [API Documentation](#api-documentation)
- [Endpoints](#endpoints)
- [Contributing](#contributing)
- [License](#license)
- [Notes](#notes)
- [Contact](#contact)

## About the Project
ToDoApp is a RESTful API built with ASP.NET Core 8.0 that allows users to manage their to-do items and boards. 
The application supports user authentication and authorization using JWT tokens, enabling secure access to the API endpoints. 
It leverages Entity Framework Core for database interactions and SQL Server Express as the database backend. 
The API is documented using Swagger (OpenAPI) for easy exploration and testing of endpoints. 

This project was developed as part of a university educational practice organized by ELEKS (remote format). In my case, the practice was formally supervised through a private entrepreneur (FOP).

## Technologies Used
- ASP.NET Core 8.0
- Entity Framework Core 8.0.7
- SQL Server Express
- Swagger (OpenAPI)
- Docker & Docker Compose

## Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Entity Framework Core Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Installation
1. Clone the repository:
   ```bash
    git clone https://github.com/Rodtzdream/ToDoApp.git
   ```
2. Navigate to the project directory:
   ```bash
    cd ToDoApp
    ```
3. Restore the NuGet packages:
   ```bash
   dotnet restore
   ```
4. Build the project:
   ```bash
   dotnet build
   ```
5. Update the database connection string in `ToDoApp.Api/appsettings.Development.json`.
   ```
   "ConnectionStrings": {
       "ToDoAppDb": "...Your_Connection_String_Here..."
     }
   ```
6. Apply database migrations (check that EF Core tools are installed):
   ```bash
   dotnet ef database update --project ToDoApp.Data --startup-project ToDoApp.Api
   ```
   
### Running the Application Locally
1. Run the application:
   ```bash
    dotnet run --project ToDoApp.Api
   ```
2. Open your browser and navigate to `https://localhost:5150/swagger` to access the Swagger UI.

### Running with Docker Compose
1. Copy '.env.example' to '.env' and change the values as needed:
   ```bash
    cp .env.example .env
   ```
2. Run the following command in the project directory where `docker-compose.yml` is located:
   ```bash
   docker-compose up -d
   ```
3. Access the application at `http://localhost:5150/swagger`.

## API Documentation
The API is documented using Swagger (OpenAPI). Once the application is running, you can access the Swagger UI at:
```
http://localhost:5150/swagger
```
This interface allows you to explore the available endpoints, view request/response schemas, and test the API directly from your browser.

## Endpoints
### Boards
- `GET /api/boards` - Retrieve all boards
- `GET /api/boards/{id}` - Retrieve a specific board by ID
- `POST /api/boards` - Create a new board

### Identity
- 'POST /register' - Register a new user
- 'POST /login' - Authenticate a user and obtain a JWT token
- 'POST /refresh' - Refresh JWT token
- 'GET /confirmEmail' - Confirm user email
- 'POST /forgotPassword' - Initiate password reset process
- 'POST /resetPassword' - Reset user password
- 'POST /manage/2fa' - Manage two-factor authentication settings
- 'GET /manage/info' - Retrieve user account information
- 'POST /manage/info' - Update user account information

### ToDoItems
- `GET /api/to-do-items` - Retrieve all to-do items
- 'POST /api/to-do-items' - Create a new to-do item
- `GET /api/to-do-items/{id}` - Retrieve a specific to-do item by
- 'DELETE /api/to-do-items/{id}' - Delete a specific to-do item by ID
- 'PUT /api/to-do-items/title' - Update the title of a to-do item
- 'PUT /api/to-do-items/description' - Update the description of a to-do item
- 'PUT /api/to-do-items/status' - Update the status of a to-do item
- 'PUT /api/to-do-items/assignee' - Update the assignee of a to-do item

### Users
- `GET /api/users` - Retrieve all users
- `GET /api/users/{id}` - Retrieve a specific user by ID

## Contributing
Contributions are welcome! Please fork the repository and create a pull request with your changes. Ensure to follow the coding standards and include tests for new features.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Notes
This project was developed with assistance from AI tools (e.g. GitHub Copilot, ChatGPT, Claude).

## Contact
- **GitHub:** [Rodtzdream](https://github.com/Rodtzdream)
- **Email:** <olijnikura@gmail.com>
- **LinkedIn:** [Yurii Oliinyk](https://www.linkedin.com/in/yurii-oliinyk-a3b891292/)