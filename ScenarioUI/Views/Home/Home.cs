using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ScenarioUI.Views.Home
{
    partial class Home
    {
        public readonly IEnumerable<string> Tests;

        public Home(HttpContext httpContext,
            IEnumerable<string> tests)
        {
            HttpContext = httpContext;
            Tests = tests;
        }
    }
}
