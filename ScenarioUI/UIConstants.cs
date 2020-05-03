using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioUI
{
    internal static class UIConstants
    {
        public static IEnumerable<string> JavaScriptBundle = new[]
        {
            "ScenarioUI.Resources.scripts.jquery-3.3.1.min.js",
            "ScenarioUI.Resources.scripts.scenario.js",
            "ScenarioUI.Resources.scripts.fontawesome.all.min.js",
            "ScenarioUI.Resources.scripts.bootstrap.bundle.min.js",
        };

        public static IEnumerable<string> CSSBundle = new[]
        {
            "ScenarioUI.Resources.styles.bootstrap.min.css"
        };

        public const string JavaScriptMediaType = "text/javascript";
        public const string CSSMediaType = "text/css";
    }
}
