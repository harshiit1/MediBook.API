# Medicare.API

A modular enterprise application built with .NET using **CQRS Pattern & Clean Architecture** principles. 
This structure separates concerns into distinct layers, maximizing maintainability, testability, and scalability.

---

# Project Architecture

The solution is divided into four main layers:

* **Medicare.API**: The entry point of the application. It handles HTTP requests, authentication, routing, and exposes RESTful API endpoints.
* **Medicare.Application**: Contains all business logic, interfaces, use cases, DTOs, and CQRS handlers (if applicable). It depends only on the Domain layer.
* **Medicare.DAL (Data Access Layer)**: Handles data persistence, database contexts ( Dapper ) & repositories.
* **Medicare.Domain**: The core of the application. Contains enterprise logic, entities, value objects, enums, and exceptions. It has zero external dependencies.

---
# How to Run

1. Open the solution via the `Medicare.api.slnx` file.
2. Right-click **Medicare.API** in the Solution Explorer and select **Set as Startup Project**.
3. Press `F5` or click **Start** to launch the API and view the Swagger/OpenAPI documentation page.
