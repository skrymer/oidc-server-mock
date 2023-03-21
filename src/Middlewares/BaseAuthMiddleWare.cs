using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Configuration;

#pragma warning disable 1591

namespace OpenIdConnectServer.Middlewares
{
    public class BaseAuthMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IdentityServerOptions _options;

        public BaseAuthMiddleWare(RequestDelegate next, IdentityServerOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.Request.Path.Value.Contains("revocation")){
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("mock-client-id:mock-client-secret");
                var base64Encoded = System.Convert.ToBase64String(plainTextBytes);
                context.Response.Headers.Add("Authorization", "Basic " +  base64Encoded);
                context.Request.Headers["Authorization"] = "Basic " + base64Encoded;
            }
            await _next(context);
        }
    }
}
