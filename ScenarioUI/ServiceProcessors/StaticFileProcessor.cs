using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ScenarioUI.ViewGenerators;

namespace ScenarioUI.ServiceProcessors
{
    internal class StaticFileProcessor : ServiceProcessor
    {
        internal const string ProcessorName = "staticfile";

        protected override async Task ProcessGetMethod(HttpContext httpContext, string actionName)
        {
            switch (actionName)
            {
                case "javascript":
                    await ResourceHelper.LoadJavaScripts(httpContext, UIConstants.JavaScriptBundle);
                    break;
                case "scenariolist":
                    await ResourceHelper.LoadJavaScripts(httpContext, UIConstants.ScenarioListJSBundle);
                    break;
                case "scenariocreator":
                    await ResourceHelper.LoadJavaScripts(httpContext, UIConstants.ScenarioCreatorJSBundle);
                    break;
                case "css":
                    await ResourceHelper.LoadStyleSheets(httpContext, UIConstants.CSSBundle);
                    break;
                default:
                    throw RouteException(httpContext);
            }
        }

        protected override async Task ProcessPostMethod(HttpContext httpContext, string actionName)
        {
            switch (actionName)
            {
                default:
                    throw RouteException(httpContext);
            }
        }
    }
}