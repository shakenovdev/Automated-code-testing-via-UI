using BL;
using Microsoft.AspNetCore.Http;
using Scenario.Annotations;
using System.Reflection;
using System.Threading.Tasks;

namespace ScenarioUI
{
    public class ScenarioUIMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ScenarioOptions _options;
        private readonly ScenarioRouting _routing;

        public ScenarioUIMiddleware(
            RequestDelegate next,
            ScenarioOptions options)
        {
            _next = next;
            _options = options;
            var serviceProvider = ServiceContainer.BuildServiceProvider();
            _routing = new ScenarioRouting(serviceProvider, _options.Root);
            ReflectedModel.Build(Assembly.GetEntryAssembly());
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var isRoutedSuccessfully = await _routing.TryProcessRoute(httpContext);
            if (isRoutedSuccessfully)
            {
                return;
            }

            await _next.Invoke(httpContext);
        }
    }
}