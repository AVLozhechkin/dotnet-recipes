# gRPC Client Management

This repository demonstrates a structured approach to managing gRPC clients in .NET applications.

## 🚀 Key Features

- **Shared Contracts via NuGet Packages**  
  The `Contracts.Clients` project is distributed as a NuGet package. It can be imported into any service that needs to interact with the gRPC server.

- **Service Interface Abstraction**  
  Instead of using the raw gRPC-generated client directly, we provide a concrete implementation of an `IService` interface. This design enables:
    - Adding basic validation logic before sending gRPC requests.
    - Easier mocking of services for integration or unit testing.

- **Interface Shared Between Client and Server**  
  The `IService` interface is shared between both the client and the server projects. This guarantees at compile time that both ends implement the same contract, improving reliability and developer experience.

## 📦 Project Structure

- **Contracts** – A shared project that contains common service contracts used by both the server and the client.
- **Contracts.Clients** – A project that provides gRPC client implementations, distributed via NuGet for reuse across services.
- **Contracts.Web** – The main Web API project that exposes the service endpoints.

## ✅ Benefits

- Strongly-typed API across services
- Easier testing and mocking
- Cleaner separation of concerns
- Shared validation logic

---

Feel free to clone this repo and use it as a reference for your own gRPC-based service architecture.
