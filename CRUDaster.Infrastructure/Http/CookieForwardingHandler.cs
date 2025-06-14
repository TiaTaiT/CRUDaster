using Microsoft.AspNetCore.Http;

namespace CRUDaster.Infrastructure.Http
{
    public class CookieForwardingHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
    {
        private readonly IHttpContextAccessor _ctx = httpContextAccessor;

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var httpContext = _ctx.HttpContext;
            if (httpContext != null && httpContext.Request.Headers.TryGetValue("Cookie", out var cookie))
            {
                // forward **all** cookies (including Auth0 session cookie)
                request.Headers.Add("Cookie", (string)cookie);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
