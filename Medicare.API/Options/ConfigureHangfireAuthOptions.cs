using Hangfire.Dashboard;
using System.Text;

namespace Medicare.API.Options
{
    public class ConfigureHangfireAuthOptions : IDashboardAuthorizationFilter
    {
        private readonly IWebHostEnvironment _env;
        public ConfigureHangfireAuthOptions(IWebHostEnvironment env)
        {
            _env = env;
        }
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            if (_env.IsDevelopment())
            {
                return true;
            }
            if (httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var header = authHeader.ToString();
                if (header.StartsWith("Basic "))
                {
                    var encoded = header["Basic ".Length..].Trim();
                    var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
                    var parts = decoded.Split(':', 2);

                    if (parts[0] == "hangfire" && parts[1] == "MediBook@2026!")
                        return true;
                }
            }

            httpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"Hangfire\"";
            httpContext.Response.StatusCode = 401;
            return false;
        }
    }
}
