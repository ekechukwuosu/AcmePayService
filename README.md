# AcmePayService

# AcmePayService

This services was developed using clean architecure. Command Query Responsibility Segregation Pattern was used for the creation of this service. This service has been created to cater for the following requirements:

Payment processing API to support the following actions:

● Authorization,

● Void,

● Capture,

● Get all transactions

### The Service contains the following Projects:

● AcmePayService.Api: - This is a .NetCore Web API service which handles the interface for client communication with the service.

● AcmePayService.Domain: - This is a class Library project that handles the Business Logic(BL) of the services implementation. Commands, queries as well as request and response objects are found here.

● AcmePayService.Common: - This is a class library project that houses common objects used across the enter service ecosystem. Enums, helper classes and methods as well as static constant variables are found here.

● AcmePayService.Infrastructure: - This is a class library project that serves as the Layer that houses Data (DATA) as well as Data Access (DAL) of the project. Thae Data Access Layer is the go-between between the Businss Logic Layer and the Data Layer. 
The repositories are found here. The Data Layer consists of Models, DBContexts. The connection to the Database is through EntityFrameworkCore. Migrations are also found in this project

● AcmePayService.Tests: This is a class library project that is houses test for the service.

