
# REST API for Order Management

This is a small REST API for managing service orders for a fictional small electronics repair shop. The API was originally developed using Spring Boot with a MySQL database and has been ported to C#.

**PLEASE NOTE:** The project was originally written in Java but has been fully ported to C#.

---

## Overview

The API allows clients to:  

1. Register a **Client** in the database.  
2. Register a **Service Order** linked to an existing client.  

The main entities are:  

- **Client**: Stores customer information.  
- **Service Order**: Stores service requests for a client.  
- **Comment**: Linked to a service order.  

### Entities

**Client**  
- `id` (GUID)  
- `name`  
- `email`  
- `phone`  

**Service Order**  
- `id` (GUID)  
- `client_id`  
- `description`  
- `price`  
- `status`  
- `createdAt`  
- `completedAt`  

**Comment**  
- `id` (GUID)  
- `description`  
- `sentDate`  
- `service_order_id`  

---

## API Endpoints

### Client Endpoints

#### Register a Client

    POST localhost:8080/api/client
**Request body example:**

    {
    "name": "John Doe",
    "email": "johndoe@example.com",
    "phone": "123456789"
    }

*Note: Phone validation is not enforced; any string is accepted. Email validation only checks the format, not the actual existence of the email or domain.*

<br/>
The response of the registration request returns a JSON with the client data, including the ID created in the database. For example, for the request above, a possible response would be:

   
     {
      "id": "b6c3f2d1-8e2a-4a67-bc18-1a2c1c3d4f5a",
      "name": "John Doe",
      "email": "johndoe@example.com",
      "phone": "123456789"
    }

***

### Retrieve a list of all registered clients

    GET localhost:8080/api/client

The response returns an array of objects with all clients registered in the database.
```
[
    {
      "id": d5cb94a8-86ff-4876-ba77-1214ab5a059c,
      "name": "John Doe",
      "email": "johndoe@example.com",
      "phone": "764483414"
    },
    {
        "id": 59b7e341-31fd-44ec-a27b-45749239cc20,
        "name": "Jane Doe",
        "email": "janedoe@example.com",
        "phone": "986679053"
    }
]
```

## Service Order Endpoint

    localhost:8080/api/ordens-servico

**Um exemplo de corpo de requisição com uma ordem de serviço válida é:**

    {
    	"client": {
    		"id": 19ff84e3-c60f-49c7-8487-8d3a9d445173
    	},
    	"description": "Smartphone repair",
    	"price": 499.99
    }
    
*The client ID must match the ID of a previously registered client.*

**Retorno do endpoint de cadastro de Ordem de Serviço**
Para o exemplo acima, o retorno do cadastro de Ordem de Serviço seria:

    {
      "id": 19ff84e3-c60f-49c7-8487-8d3a9d445173,
      "client": {
        "id": d5cb94a8-86ff-4876-ba77-1214ab5a059c,
        "name": "John Doe",
      },
      "description": "Smartphone repair",
      "price": 499.99,
      "status": "OPEN",
      "dateOpen": "2021-01-06T22:30:21.351941-03:00",
      "dateClosed": null
    }
A Service Order is automatically created with a status of OPEN, dataAbertura set to the request date, and dataFinalizacao left blank. This field is updated when the Service Order is closed via its corresponding endpoint.
