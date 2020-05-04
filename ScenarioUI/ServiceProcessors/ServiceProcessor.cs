using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScenarioUI.ViewGenerators;

namespace ScenarioUI.ServiceProcessors
{
    internal abstract class ServiceProcessor
    {
        public async Task<bool> Process(HttpContext httpContext, string actionName)
        {
            var httpMethod = httpContext.Request.Method;

            switch (httpMethod)
            {
                case "GET":
                    await ProcessGetMethod(httpContext, actionName);
                    return true;
                case "POST":
                    await ProcessPostMethod(httpContext, actionName);
                    return true;
                default:
                    // return error page
                    return false;
            }
        }

        protected abstract Task ProcessGetMethod(HttpContext httpContext, string actionName);

        protected abstract Task ProcessPostMethod(HttpContext httpContext, string actionName);

        public static ServiceProcessor CreateProcessor(IServiceProvider serviceProvider, string processorName)
        {
            switch (processorName)
            {
                case ScenarioCreatorServiceProcessor.ProcessorName:
                    return new ScenarioCreatorServiceProcessor(serviceProvider);
                case ScenarioExecutorServiceProcessor.ProcessorName:
                    return new ScenarioExecutorServiceProcessor(serviceProvider);
                case StaticFileProcessor.ProcessorName:
                    return new StaticFileProcessor();
                default:
                    return new ScenarioListServiceProcessor(serviceProvider);
            }
        }

        protected static async Task GenerateView(HttpResponse httpResponse, WebViewGenerator view)
        {
            httpResponse.StatusCode = 200;
            httpResponse.ContentType = "text/html;charset=utf-8";

            await httpResponse.WriteAsync(view.TransformText());
        }

        protected static RouteCreationException RouteException(HttpContext httpContext)
        {
            return new RouteCreationException($"{httpContext.Request.Path.Value} is invalid route");
        }
    }
}
