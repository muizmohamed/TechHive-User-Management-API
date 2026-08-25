# UserManagementAPI

ASP.NET Core Web API (.NET 9) for TechHive Solutions user management.

## Features

- CRUD endpoints for users.
- EF Core + SQLite persistence.
- Input validation using DataAnnotations and `[ApiController]`.
- Duplicate-email protection.
- 404 handling for missing users.
- Centralized JSON exception handling.
- Bearer-token authentication middleware.
- Request/response status logging middleware.
- Swagger UI in Development.
- Async database operations and `AsNoTracking()` for read-only GET requests.

## Run

1. Open the folder in Visual Studio 2022 or VS Code.
2. Make sure .NET 9 SDK is installed.
3. Run:

```bash
dotnet restore
dotnet run
```

Swagger should open at `https://localhost:7041/swagger` when using the supplied launch profile.

## Authentication

Use this demo token:

```text
Authorization: Bearer techhive-demo-token
```

For production, store tokens/secrets in a secure secret store and use a proper identity provider/JWT validation.

## CRUD Endpoints

| Method | Endpoint | Purpose |
|---|---|---|
| GET | `/api/users` | Get all users |
| GET | `/api/users/{id}` | Get one user |
| POST | `/api/users` | Create a user |
| PUT | `/api/users/{id}` | Update a user |
| DELETE | `/api/users/{id}` | Delete a user |

## Sample POST/PUT body

```json
{
  "firstName": "Ahmed",
  "lastName": "Ali",
  "email": "ahmed.ali@example.com",
  "department": "IT"
}
```

## Postman test plan

Set a collection-level header:

```text
Authorization: Bearer techhive-demo-token
Content-Type: application/json
```

Test these cases:

1. GET `/api/users` -> 200.
2. POST valid user -> 201.
3. POST duplicate email -> 409.
4. POST blank/invalid email -> 400.
5. GET existing id -> 200.
6. GET missing id -> 404.
7. PUT existing id with valid data -> 200.
8. PUT missing id -> 404.
9. DELETE existing id -> 204.
10. DELETE missing id -> 404.
11. Missing token -> 401.
12. Invalid token -> 401.

## Middleware order

The pipeline is configured in the requested order:

1. Error handling middleware.
2. Token authentication middleware.
3. Request logging middleware.

Authentication failures are also written to logs so denied requests are auditable even though they do not enter the downstream logging middleware.

## How Copilot was used/documented

Suggested prompts used during development:

- Analyze the CRUD controller and identify missing validation and error handling.
- Add validation attributes to the user input DTOs.
- Improve GET performance using async EF Core queries and `AsNoTracking()`.
- Add duplicate-email protection for POST and PUT.
- Create centralized exception-handling middleware returning JSON.
- Create Bearer-token authentication middleware returning 401 for invalid tokens.
- Create request logging middleware for method, path, status code, and elapsed time.
- Review the middleware order and explain side effects.

The developer should review and test Copilot-generated code before committing it.

## GitHub submission

```bash
git init
git add .
git commit -m "Create UserManagementAPI CRUD and middleware"
git branch -M main
git remote add origin https://github.com/<YOUR-USERNAME>/UserManagementAPI.git
git push -u origin main
```
