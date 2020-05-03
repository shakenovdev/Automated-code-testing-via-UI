using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BL;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using ScenarioUI.ViewGenerators;
using ScenarioUI.Views.Home;

namespace ScenarioUI
{
    internal class ScenarioRouting
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _root;
        private const string HOME_PAGE = "list";

        internal ScenarioRouting(IServiceProvider serviceProvider, string root)
        {
            _serviceProvider = serviceProvider;
            _root = root.FirstOrDefault() == '/' ? root : '/' + root;
        }

        internal async Task<bool> TryProcessRoute(HttpContext httpContext)
        {
            var httpRequest = httpContext.Request;

            var path = httpRequest.Path.Value;
            if (!IsScenarioRoute(path, out var route))
            {
                return false;
            }

            var httpMethod = httpRequest.Method;

            switch (httpMethod)
            {
                case "GET":
                    await ProcessGETmethod(httpContext, route);
                    return true;
                case "POST":
                    return true;
                default:
                    // return error page
                    return false;
            }
        }

        private bool IsScenarioRoute(string path, out string route)
        {
            var isScenarioHomePage = path.Equals(_root, StringComparison.InvariantCultureIgnoreCase);
            if (isScenarioHomePage)
            {
                route = HOME_PAGE;
                return true;
            }

            var isScenarioPage = path.StartsWith(_root, StringComparison.InvariantCultureIgnoreCase);
            if (isScenarioPage)
            {
                route = path.Split('/').LastOrDefault();
                return true;
            }

            route = string.Empty;
            return false;
        }

        private async Task ProcessGETmethod(HttpContext httpContext, string route)
        {
            switch (route.ToLowerInvariant())
            {
                case HOME_PAGE:
                    //var scenarioListService = (IScenarioListService)_serviceProvider.GetService(typeof(IScenarioListService));
                    //var scenarioList = scenarioListService.GetAll().RootScenarios.Select(x => x.Namespace).ToList();
                    var strings = new List<string> { "its", "fucking", "working" };
                    var view = new Home(httpContext, strings);
                    await GenerateView(httpContext.Response, view);
                    break;
                case "javascript":
                    await ResourceHelper.LoadJavaScripts(httpContext);
                    break;
                case "css":
                    await ResourceHelper.LoadStyleSheets(httpContext);
                    break;
                default:
                    // show error page
                    break;
            }
        }

        private static async Task GenerateView(HttpResponse httpResponse, WebViewGenerator view)
        {
            httpResponse.StatusCode = 200;
            httpResponse.ContentType = "text/html;charset=utf-8";

            await httpResponse.WriteAsync(view.TransformText());
        }
    }
}
