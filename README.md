
# REST API for Order Management

This is a small REST API for managing service orders for a fictional small electronics repair shop. The API was originally developed using Spring Boot with a MySQL database and has been ported to C#.

This project was originally written in Java but has been fully ported to C#.

## Overview

The main API route is 
```
http://localhost:5000/api
```

To access the Swagger UI for testing the API endpoints, navigate to:
```
http://localhost:5000/swagger
```


## Tools used
Tools used for this project:
.Net 8 as the framework, Entity Framework Core for ORM, and SQLite as the database.
Swagger is used for API documentation and testing.

## Getting Started
To run the project locally, follow these steps:
1. Clone the repository:
   ```bash
   git clone
   ```
2. Navigate to the project directory:
   ```bash 
   cd OrderManagementAPI
   ```

3. Restore the dependencies:
   ```bash
   dotnet restore
   ```

4. Run the application:
   ```bash
   dotnet run
   ```
5. Open your browser and navigate to `http://localhost:5000/swagger` to access the Swagger UI for testing the API endpoints.

You can also pull the Docker image from Docker Hub and run it using Docker:
```bash
docker pull jpmorais/order-managementapi:latest
docker run -d -p 5000:5000 jpmorais/order-managementapi:latest
```
