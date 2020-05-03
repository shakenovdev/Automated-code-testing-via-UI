using System;
using Microsoft.AspNetCore.Builder;
using Scenario.Annotations;
using ScenarioUI;

namespace Scenario.Core
{
    public static class ScenarioBuilderExtensions
    {
        public static IApplicationBuilder UseScenario(this IApplicationBuilder app)
        {
            return UseScenario(app, options => new ScenarioOptions());
        }

        public static IApplicationBuilder UseScenario(this IApplicationBuilder app, Action<ScenarioOptions> configureOptions)
        {
            var options = new ScenarioOptions();
            configureOptions(options);

            return app.UseMiddleware<ScenarioUIMiddleware>(options);
        }
    }
}