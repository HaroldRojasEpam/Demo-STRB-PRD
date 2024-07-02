# Demo-STRB-PRD
 MinimalAPI Demo, which includes Unit and Integration Tests

# MinimalAPI
MinimalAPI demo realized with clean architecture, applying as design pattern the command query responsibility segregation (CQRS). The database chosen for this demo is SQL Server 2022.

## Dependencies
The project was created in .NET 8 and uses the following packages
- MediatR: this library is used for the implementation of the mediator pattern, according to the CQRS design pattern, reducing the complexity of communication between classes.
- EF Core: used for the relational mapping of objects, allowing access to the database, and even the migration of tables to the required database is achieved.
- Shouldly: this library is used for object verification or assertion during unit and integration testing.
- Testcontainers: used for creating temporary or disposable instances during integration testing, loading the images into docker during the process and then deleting them. 
- NSubstitute: this library is used to speed up unit and integration tests, saving the test creation process. 

# Project structure

## MinimalAPI
For the development of the MinimalAPI, a clean architecture framework was granted, which is composed of the following components
- StarbucksDemo: is the MinimalAPI
- Application: in this layer hosts the command and query classes, according to the CQRS pattern.
- Infraestructure: This layer hosts the interaction with the database.
- Core: Stores the user's entities, DTO's and exceptions.

## Test projects
For the projects that are in charge of unit and integration testing, it is conformed as follows
- StarbucksTestDemo.API.Unit
- StarbucksTestDemo.API.Integration

# Docker
This project has the docker orchestrator (docker-compose), allowing to create containers for the database, the MinimalAPI, as well as being used for integration testing.
