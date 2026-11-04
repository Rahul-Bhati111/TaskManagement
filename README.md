# Task Management System

A web-based Task Management application built using ASP.NET Core Razor Pages, C#, Dapper, and SQL Server.

The application allows users to create, view, update, assign, and track tasks through a simple and structured interface.

---

## 1. Technology Stack

| Component | Technology |
|---|---|
| Backend | ASP.NET Core 8 |
| Programming Language | C# |
| UI Framework | Razor Pages |
| Frontend | HTML, CSS, Bootstrap |
| Client-side scripting | jQuery |
| Data Access | Dapper |
| Database | Microsoft SQL Server |
| Database Operations | Stored Procedures |
| Dependency Injection | ASP.NET Core DI |
| Logging | ASP.NET Core ILogger (Serilog planned) |

---

## 2. Project Architecture

The solution follows a layered architecture to maintain separation of concerns, loose coupling, and maintainability.

```text
TaskManagement
│
├── src
│   │
│   ├── TaskManagement.Web
│   │   ├── Pages
│   │   ├── wwwroot
│   │   ├── appsettings.json
│   │   └── Program.cs
│   │
│   ├── TaskManagement.Application
│   │   ├── DTOs
│   │   ├── Interfaces
│   │   ├── Services
│   │   └── DependencyInjection.cs
│   │
│   └── TaskManagement.Infrastructure
│       ├── Data
│       ├── Repositories
│       └── DependencyInjection.cs
│
├── tests
│   └── TaskManagement.Tests
│
├── Database
│   ├── 01_CreateDatabase.sql
│   ├── 02_CreateTables.sql
│   ├── 03_SeedLookupData.sql
│   ├── 04_CreateIndexes.sql
│   └── StoredProcedures
│
└── README.md
```

### Layer Responsibilities

**TaskManagement.Web**
- Handles HTTP requests through Razor Pages.
- Contains UI and PageModels.
- Uses Bootstrap for styling.
- Communicates with Application services through dependency injection.

**TaskManagement.Application**
- Contains business services and interfaces.
- Defines DTOs used across application layers.
- Performs application-level validation.
- Does not depend on Infrastructure or Web.

**TaskManagement.Infrastructure**
- Implements repository interfaces.
- Handles database connectivity using Dapper.
- Executes SQL Server stored procedures.
- Contains the SQL connection factory.

**TaskManagement.Tests**
- Reserved for unit and integration tests.

### Application Flow

```text
User
  |
  v
Razor Pages (.cshtml)
  |
  v
PageModel (.cshtml.cs)
  |
  v
Application Services
  |
  v
Repository Interfaces
  |
  v
Infrastructure Repositories
  |
  v
Dapper
  |
  v
SQL Server Stored Procedures
  |
  v
TaskManagementDB
```

---

## 3. Implemented Features

### Task Management

- Create a new task.
- View all tasks in a listing.
- View individual task details.
- Edit existing tasks.
- Update task title and description.
- Update task due date.
- Change task priority.
- Change task status.
- Assign tasks to users.
- Reassign tasks to other users.
- Remove task assignments.
- Edit closed tasks.

### Task Listing and Filtering

- Display task ID.
- Display task title.
- Display due date.
- Display priority.
- Display assigned user.
- Display current status.
- Filter tasks by assigned user.
- View unassigned tasks.
- Reset filters.

### Lookup Management

Priority and status values are stored in database lookup tables and populated dynamically.

**Default Priorities**
- Low
- Medium
- High

**Default Statuses**
- Open
- In Progress
- Hold
- Closed

### User Management

Users are currently managed directly in SQL Server.

User information includes:
- User ID
- Full Name
- Email
- Date of Joining
- Active Status

Only active users are available for new assignments. Existing assignments to inactive users can be retained.

---

## 4. Database Design

Database name: `TaskManagementDB`

### Tables

| Table | Description |
|---|---|
| AppUsers | Stores application users |
| Tasks | Stores task information |
| TaskPriorities | Stores configurable priority values |
| TaskStatuses | Stores configurable status values |

### Tasks Table

Important fields:

| Column | Description |
|---|---|
| TaskId | Primary key |
| Title | Task title |
| Description | Task description |
| DueDate | Task due date and time |
| PriorityId | Foreign key to TaskPriorities |
| AssignedUserId | Optional foreign key to AppUsers |
| StatusId | Foreign key to TaskStatuses |
| CreatedAt | Record creation timestamp |
| UpdatedAt | Last modification timestamp |

### Database Design Principles

- Primary and foreign key relationships.
- Lookup tables for configurable priority and status values.
- Nullable AssignedUserId to support unassigned tasks.
- Indexes on AssignedUserId, StatusId, and PriorityId.
- UTC timestamps for record creation and modification.

---

## 5. Stored Procedures

All application database operations are performed using stored procedures. Inline SQL queries are not used in application repositories.

| Stored Procedure | Responsibility |
|---|---|
| usp_Task_Create | Creates a new task |
| usp_Task_Update | Updates an existing task |
| usp_Task_GetById | Retrieves task details |
| usp_Task_GetAll | Retrieves tasks with optional filters |
| usp_User_GetActiveUsers | Retrieves active users |
| usp_TaskPriority_GetAll | Retrieves active priorities |
| usp_TaskStatus_GetAll | Retrieves active statuses |
| usp_Task_GetDashboardSummary | Retrieves dashboard summary data |

The dashboard summary procedure is created in the database; its UI integration is planned for a later phase.

---

## 6. Prerequisites

Install the following before running the application:

- .NET 8 SDK
- Microsoft SQL Server
- SQL Server Management Studio (SSMS)
- Visual Studio 2022 or a compatible IDE
- Git

---

## 7. Database Setup

1. Clone the repository.
2. Open SQL Server Management Studio.
3. Connect to your SQL Server instance.
4. Navigate to the `Database` folder.
5. Execute the database scripts in the following order:

| Order | Script | Purpose |
|---|---|---|
| 1 | 01_CreateDatabase.sql | Creates TaskManagementDB |
| 2 | 02_CreateTables.sql | Creates database tables and relationships |
| 3 | 03_SeedLookupData.sql | Inserts default priorities and statuses |
| 4 | 04_CreateIndexes.sql | Creates database indexes |
| 5 | StoredProcedures folder | Creates all stored procedures |

Ensure all scripts execute successfully before running the application.

---

## 8. Application Configuration

Open:

`src/TaskManagement.Web/appsettings.json`

Configure the SQL Server connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=TaskManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER_NAME` with your SQL Server instance name.

For SQL Server authentication, configure the appropriate username and password securely. Never commit production credentials to Git.

---

## 9. Running the Application

Clone the repository:

```bash
git clone YOUR_REPOSITORY_URL
```

Navigate to the solution directory:

```bash
cd TaskManagement
```

Restore NuGet packages:

```bash
dotnet restore
```

Build the solution:

```bash
dotnet build
```

Run the web application:

```bash
dotnet run --project src/TaskManagement.Web
```

Open the HTTPS URL displayed in the terminal.

### Available Pages

| Page | Route | Description |
|---|---|---|
| Task Listing | `/TaskList` | Lists and filters tasks |
| Create Task | `/Create` | Creates a new task |
| Task Details | `/Details/{id}` | Views and updates an existing task |

---

## 10. Architecture and Development Principles

The application follows these principles:

- Separation of concerns.
- Dependency Injection.
- SOLID principles.
- Interface-based repository abstraction.
- Asynchronous database operations.
- DTO-based communication between layers.
- Centralized database connection management.
- Stored-procedure-based database access.
- Application-level and database-level validation.

Repository interfaces are defined in the Application layer and implemented in Infrastructure. This avoids direct dependencies from the Application layer to database implementation details.

---

## 11. Future Enhancements

The following features are planned for subsequent development phases:

- Dashboard with total task count.
- Status-wise task summary.
- Priority-wise task summary.
- User-wise task summary.
- Search tasks by Task ID.
- Improved task details and read-only view mode.
- Serilog file-based logging.
- Serilog database logging.
- Improved exception handling and user-friendly error messages.
- Unit and integration tests.
- Pagination and additional task filters.
- Deployment configuration.

---

## 12. Current Project Status

| Feature | Status |
|---|---|
| Solution and project structure | Completed |
| Layered architecture | Completed |
| SQL Server database design | Completed |
| Stored procedures | Completed |
| Dapper integration | Completed |
| Dependency Injection | Completed |
| Task listing | Completed |
| User and task filtering | Completed |
| Create Task | Completed |
| View Task Details | Implemented |
| Edit Task | Implemented |
| Dashboard UI | Planned |
| Logging with Serilog | Planned |
| Automated tests | Planned |

---

## 13. Contribution

Contributions and suggestions are welcome.

When making database changes, update the corresponding SQL scripts and document the execution order.

When adding new functionality, maintain the existing layered architecture and use stored procedures for database operations.

---

**Task Management System**  
Built with ASP.NET Core, Dapper, and SQL Server.