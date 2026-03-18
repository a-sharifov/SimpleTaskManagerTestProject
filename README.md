# Simple Task Manager

CRUD app for task management.

---
## Getting Started

### Requires 
- [Docker](https://docs.docker.com/get-docker/) & Docker Compose.
- Visual Studio 2026 or later with .NET 10 SDK.

### Run
```bash
git clone https://github.com/a-sharifov/SimpleTaskManagerTestProject.git
cd SimpleTaskManagerTestProject
docker compose -f docker-compose.test.yml up -d --build
```

Open [http://localhost:8080](http://localhost:8080)

> if you want default seed data you need change 
> in docker-compose.test.yml file `USE_SEED_DATA` env variable to `true` for api service.
    
Environment variables are set in `.env` (defaults are pre-filled).

> **Note:** The `.env` file is committed intentionally since this is a test project.
> In production, use Azure Key Vault, Docker Secrets, HashiCorp Vault, or CI/CD environment variables.
---

## Endpoints

```
GET  /                          → Task list (pagination, sorting)
GET  /TaskModels/Details/{id}   → Task details
GET  /TaskModels/Create         → Create form
POST /TaskModels/Create         → Create task
GET  /TaskModels/Edit/{id}      → Edit form
POST /TaskModels/Edit/{id}      → Update task
GET  /TaskModels/Delete/{id}    → Delete confirmation
POST /TaskModels/Delete/{id}    → Delete task
```
---

## Stack

- .NET 10, ASP.NET Core MVC, C# 14
- PostgreSQL 17 + Entity Framework Core 10
- MediatR (CQRS), Ardalis.Result, Ardalis.SmartEnum, StronglyTypedId
- Docker Compose
---

## Architecture


```
src/
├── Domain/
│   ├── SharedKernel/
│   │   └── Paginations/
│   └── TaskModelAggregate/
│       ├── Enumerations/        
│       ├── Errors/               
│       ├── Ids/                 
│       ├── Projections/          
│       ├── Repositories/         
│       ├── Specifications/       
│       ├── ValueObjects/         
│       └── TaskModel.cs
├── Application/
│   ├── Common/CQRS/             
│   └── TaskModels/
│       ├── Commands/             
│       └── Queries/      
├── Persistence/
│   ├── DbContexts/             
│   ├── Repositories/            
│   ├── Seeds/                   
│   └── UnitOfWorks/             
└── Api/
    ├── Controllers/             
    ├── Extensions/              
    ├── Models/                  
    ├── Views/                   
    └── Program.cs
```

</details>

