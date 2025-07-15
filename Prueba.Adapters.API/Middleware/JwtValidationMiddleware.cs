using System.IdentityModel.Tokens.Jwt;

namespace Prueba.Adapters.API.Middleware
{
    public class JwtValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;

            // Permitir solicitudes OPTIONS (preflight)
            if (context.Request.Method == HttpMethods.Options)
            {
                SetCorsHeaders(context);
                context.Response.StatusCode = 200;
                return;
            }

            // Permitir acceso sin token a rutas públicas como Swagger
            if (path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                SetCorsHeaders(context);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token no encontrado.");
                return;
            }

            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Validar expiración (opcional)
                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    SetCorsHeaders(context);
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token expirado.");
                    return;
                }

                // Si el token es válido, continúa con la solicitud
                await _next(context);
            }
            catch
            {
                SetCorsHeaders(context);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token inválido.");
            }
        }

        private void SetCorsHeaders(HttpContext context)
        {
            context.Response.Headers["Access-Control-Allow-Origin"] = "http://localhost:4200";
            context.Response.Headers["Access-Control-Allow-Headers"] = "Content-Type, Authorization";
            context.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";
            context.Response.Headers["Access-Control-Allow-Credentials"] = "true";
        }
    }
}
