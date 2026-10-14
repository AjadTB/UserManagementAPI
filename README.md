# UserManagementAPI

A User Management API for TechHive Solutions' HR and IT departments, built with ASP.NET Core (.NET 8) and Microsoft Copilot.

## Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/users` | List all users |
| GET | `/api/users/{id}` | Get one user (404 if missing) |
| POST | `/api/users` | Create a user (400 invalid, 409 duplicate email) |
| PUT | `/api/users/{id}` | Update a user |
| DELETE | `/api/users/{id}` | Delete a user (204, or 404 if missing) |

## Validation
Name (2-100 chars), valid email, age 18-100 and department are required. Duplicate emails (case-insensitive) are rejected.

## Middleware (in pipeline order)
1. **ErrorHandlingMiddleware** - catches unhandled exceptions and returns a JSON 500
2. **RequestLoggingMiddleware** - logs method, path, status code and timing for every request
3. **TokenAuthenticationMiddleware** - requires `Authorization: Bearer <token>` (set `Auth:Token`, default is for local testing only)

## Run it
```bash
dotnet run
```
Then use `UserManagementAPI.http` (VS Code REST Client / Visual Studio) to try the endpoints. Test token: `secret-token-123`.

## How Copilot Assisted

- Helped create the user model and validation rules.
- Generated CRUD endpoints in the users controller.
- Assisted with HTTP status codes and request validation.
- Helped identify and resolve API testing issues.
