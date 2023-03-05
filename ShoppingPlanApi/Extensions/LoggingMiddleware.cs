using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Net;
using System.Xml;

namespace ShoppingPlanApi.Extensions
{
    public class LoggingMiddleware
    {
        readonly RequestDelegate _requestDelegate;
        readonly ILogger _logger;
        public LoggingMiddleware(RequestDelegate requestDelegate, ILogger logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var watch = Stopwatch.StartNew();
            try
            {
               
                await _requestDelegate(httpContext);
                watch.Stop();
            }
            catch(Exception ex)
            {
                watch.Stop();
                 HandleExcepiton(httpContext, ex, watch);
            }
            finally
            {
                string logText = $"{httpContext.Request?.Method} {httpContext.Request?.Path.Value}=>{httpContext.Response?.StatusCode}{Environment.NewLine}";
                File.AppendAllText("log.txt", logText);
            }
            
        }
        private void HandleExcepiton(HttpContext context, Exception ex, Stopwatch watch)
        {
            
            string logText = $"{context.Request?.Method} {context.Request?.Path.Value}=>{context.Response?.StatusCode}{Environment.NewLine}";
            File.AppendAllText("log.txt", logText+"\n"+ex.ToString()+"\n"+watch);
        }
    }
}
