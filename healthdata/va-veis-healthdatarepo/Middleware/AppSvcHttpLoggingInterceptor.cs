using Microsoft.AspNetCore.HttpLogging;

namespace va_veis_healthdatarepo.Middleware
{
    internal sealed class AppSvcHttpLoggingInterceptor : IHttpLoggingInterceptor
    {
        //for more info on this see the link below
        //https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-logging/?view=aspnetcore-8.0

        public ValueTask OnRequestAsync(HttpLoggingInterceptorContext logContext)
        {
            if (logContext.HttpContext.Request.Path.Value.Contains("swagger"))
            {
                logContext.LoggingFields = HttpLoggingFields.None;
            }
            else
            {
                logContext.LoggingFields = HttpLoggingFields.All;
            }

            return default;
        }

        public ValueTask OnResponseAsync(HttpLoggingInterceptorContext logContext)
        {
            if (logContext.HttpContext.Request.Path.Value.Contains("swagger"))
            {
                logContext.LoggingFields = HttpLoggingFields.None;
            }
            else
            {
                logContext.LoggingFields = HttpLoggingFields.All;
            }

            return default;
        }

        private void RedactPath(HttpLoggingInterceptorContext logContext)
        {
            logContext.AddParameter(nameof(logContext.HttpContext.Request.Path), "RedactedPath");
        }

        private void RedactRequestHeaders(HttpLoggingInterceptorContext logContext)
        {
            foreach (var header in logContext.HttpContext.Request.Headers)
            {
                logContext.AddParameter(header.Key, "RedactedHeader");
            }
        }

        private void EnrichRequest(HttpLoggingInterceptorContext logContext)
        {
            logContext.AddParameter("RequestEnrichment", "Stuff");
        }

        private void RedactResponseHeaders(HttpLoggingInterceptorContext logContext)
        {
            foreach (var header in logContext.HttpContext.Response.Headers)
            {
                logContext.AddParameter(header.Key, "RedactedHeader");
            }
        }

        private void EnrichResponse(HttpLoggingInterceptorContext logContext)
        {
            logContext.AddParameter("ResponseEnrichment", "Stuff");
        }
    }
}