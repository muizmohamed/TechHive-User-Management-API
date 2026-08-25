namespace UserManagementAPI.Middleware;

public class TokenAuthenticationMiddleware(
    RequestDelegate next,
    IConfiguration configuration,
    ILogger<TokenAuthenticationMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue("Authorization", out var authorization))
        {
            logger.LogWarning("Unauthorized request: missing Authorization header for {Path}", context.Request.Path);
            await Unauthorized(context);
            return;
        }

        const string prefix = "Bearer ";
        if (!authorization.ToString().StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        {
            logger.LogWarning("Unauthorized request: invalid authorization scheme for {Path}", context.Request.Path);
            await Unauthorized(context);
            return;
        }

        var token = authorization.ToString()[prefix.Length..].Trim();
        var expectedToken = configuration["Authentication:ApiToken"];

        if (string.IsNullOrWhiteSpace(expectedToken) ||
            !string.Equals(token, expectedToken, StringComparison.Ordinal))
        {
            logger.LogWarning("Unauthorized request: invalid token for {Path}", context.Request.Path);
            await Unauthorized(context);
            return;
        }

        await next(context);
    }

    private static async Task Unauthorized(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new
        {
            error = "Unauthorized. A valid Bearer token is required."
        });
    }
}
