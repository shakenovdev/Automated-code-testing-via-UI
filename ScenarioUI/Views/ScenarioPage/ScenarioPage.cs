using Microsoft.AspNetCore.Http;

namespace ScenarioUI.Views.ScenarioPage
{
    partial class ScenarioPage
    {
        public readonly string ReflectedCollectionJson;
        public readonly string ScenarioPageJson;

        public ScenarioPage(HttpContext httpContext, string reflectedCollectionJson, string scenarioPageJson)
        {
            HttpContext = httpContext;
            ReflectedCollectionJson = reflectedCollectionJson;
            ScenarioPageJson = scenarioPageJson;
        }
    }
}