using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScenarioUI.ServiceProcessors;
using ScenarioUI.ViewGenerators;
using ScenarioUI.Views.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScenarioUI
{
    internal class ScenarioRouting
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _root;

        internal ScenarioRouting(IServiceProvider serviceProvider, string root)
        {
            _serviceProvider = serviceProvider;
            _root = root.FirstOrDefault() == '/' ? root : '/' + root;
        }

        internal async Task<bool> TryProcessRoute(HttpContext httpContext)
        {
            var httpRequest = httpContext.Request;

            var path = httpRequest.Path.Value;
            if (!IsScenarioRoute(path, out var processorName, out var actionName))
            {
                return false;
            }

            var serviceProcessor = ServiceProcessor.CreateProcessor(_serviceProvider, processorName);
            return await serviceProcessor.Process(httpContext, actionName);
        }

        private bool IsScenarioRoute(string path, out string processorName, out string actionName)
        {
            processorName = ScenarioListServiceProcessor.ProcessorName;
            actionName = string.Empty;

            var isScenarioHomePage = path.Equals(_root, StringComparison.InvariantCultureIgnoreCase);
            if (isScenarioHomePage)
            {
                return true;
            }

            var isScenarioPage = path.StartsWith(_root, StringComparison.InvariantCultureIgnoreCase);
            if (isScenarioPage)
            {
                var routes = path.Split('/').Skip(2).ToArray(); // skip root

                if (routes.Length > 2)
                    throw new RouteCreationException($"{path} is invalid route");

                processorName = routes[0];
                actionName = routes.ElementAtOrDefault(1);
                return true;
            }
            
            return false;
        }
    }
}
