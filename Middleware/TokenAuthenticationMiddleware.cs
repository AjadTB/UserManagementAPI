namespace UserManagementAPI.Middleware;

// Requires "Authorization: Bearer <token>" on every request.
public class TokenAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _validToken;

    public TokenAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        // Set Auth:Token in appsettings or an environment variable (Auth__Token).
        // The fallback is only for local testing in this assignment.
        _validToken = configuration["Auth:Token"] ?? "secret-token-123";
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var header = context.Request.Headers.Authorization.ToString();

        if (!header.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) ||
            header["Bearer ".Length..].Trim() != _validToken)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { error = "Unauthorized. Provide a valid Bearer token." });
            return;
        }

        await _next(context);
    }
}
