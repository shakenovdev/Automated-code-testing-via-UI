using Microsoft.AspNetCore.Http;

namespace ScenarioUI.Views.Home
{
    partial class Home
    {
        public readonly string ScenarioListJson;

        public Home(HttpContext httpContext, string scenarioListJson)
        {
            HttpContext = httpContext;
            ScenarioListJson = scenarioListJson;
        }
    }
}
