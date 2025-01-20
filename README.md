# EmployerPortal

EmployerPortal is a web application built with ASP.NET Core and Entity Framework Core. 

## Table of Contents

- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
- [Seed Data](#seed-data)
- [Legacy Code Problems](#legacy-code-problems)

## Getting Started

These instructions will help you set up and run the project on your local machine for development and testing purposes.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQLite](https://www.sqlite.org/download.html)

## Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/EmployerPortal.git
    cd EmployerPortal
    ```

2. Restore the dependencies:
    ```sh
    dotnet restore
    ```

## Running the Application

1. Run the application:
    ```sh
    dotnet run
    ```

3. The application will start, and the Swagger UI will open in your default browser at `http://localhost:5299/swagger`.

## API Endpoints

### Users

- **Login**
    - **GET** `/api/users/login?username={username}`
    - Description: Logs in a user with the specified username.
    - Response: A welcome message if the username is valid, otherwise an error message.

### Seed Data
When the application is first run, it seeds the database with the following default users:

- admin
- developer
- manager

### Legacy Code Problems

### Vulnerabilities in the ASP Classic Code Snippet

- SQL Injection: The way the code puts user input (like the username) right into the SQL query creates a risk for SQL injection attacks.  The current solution in dotnet core is safe from SQL injection. The use of Entity Framework Core's with LINQ query ensures that the query is parameterized protecting against SQL injection.

- Error Handling: The code doesn’t handle errors well. If there’s a problem connecting to the database or running a query, it doesn’t capture or record the error, which could cause the application to crash or show sensitive error messages to users. The current solution don't expose sentitive error and also log the error for internal use.


