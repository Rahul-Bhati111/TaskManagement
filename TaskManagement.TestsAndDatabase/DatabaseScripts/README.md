# Database Setup – Task Management

## Prerequisites
- Microsoft SQL Server
- SQL Server Management Studio (SSMS)

## Database
Database name: `TaskManagementDB`

## Setup Instructions

Execute the following scripts in order:

1. `01_CreateDatabase.sql` – Creates the database.
2. `02_CreateTables.sql` – Creates all tables and relationships.
3. `03_SeedLookupData.sql` – Inserts default priorities and statuses.
4. `04_CreateIndexes.sql` – Creates database indexes.
5. Execute all scripts inside `StoredProcedures/` – Creates the stored procedures.

## Default Lookup Values

**Priorities**
- Low
- Medium
- High

**Statuses**
- Open
- In Progress
- Hold
- Closed

## Application Configuration

Update the connection string in `TaskManagement.Web/appsettings.json` according to your SQL Server instance.

Do not commit production credentials or passwords to Git.

## Notes
- Users are managed directly in the database.
- No sample user or task records are included.
- All application database operations use stored procedures.