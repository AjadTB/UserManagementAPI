using UserManagementAPI.Middleware;
using UserManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();

// Pipeline order matters:
// 1. Error handling first so it can catch exceptions from everything after it
// 2. Logging next so even rejected (401) requests are recorded
// 3. Authentication last, right before the controllers
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<TokenAuthenticationMiddleware>();

app.MapControllers();

app.Run();
