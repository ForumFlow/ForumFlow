# Scripts to run

## Rebuild the project

- dotnet build

## SqlUtil Commands

### Create/Recreate Tables

- dotnet run -- clearTables

### Insert DummyData (will clear tables before inserting)

- dotnet run -- insertTestData

### show all users in table

- dotnet run -- showUsers

## this will run the server

- dotnet run

---

# Folder Structure

### User

- types.cs contains the types used has one for making a user
- userAuthRoutes.cs contains the routes for making a post user

> [!NOTE]
> still working on intergrating authenication
> need to work on session tokens
