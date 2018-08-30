using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationJWT
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        public TokenAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //public async Task InvokeAsync(HttpContext context, ITraffkTenantFinder traffkTenantFinder)
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method != "OPTIONS")
            {
                var authenticateInfo = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                var bearerTokenIdentity = authenticateInfo?.Principal;
                if (bearerTokenIdentity != null)
                {
                    context.User = bearerTokenIdentity;
                    await _next(context);
                    return;
                }

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }
    }
}
