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
            "ScenarioUI.Resources.scripts.fontawesome.min.js",
            "ScenarioUI.Resources.scripts.bootstrap.bundle.min.js",
            "ScenarioUI.Resources.scripts.lodash.min.js",
            "ScenarioUI.Resources.scripts.vue.dev.js",
            "ScenarioUI.Resources.scripts.vue-input-autowidth.js",
            "ScenarioUI.Resources.scripts.axios.min.js"
            //"ScenarioUI.Resources.scripts.vue.min.js"
        };

        public static IEnumerable<string> CSSBundle = new[]
        {
            "ScenarioUI.Resources.styles.bootstrap.min.css",
            "ScenarioUI.Resources.styles.site.css"
        };

        public static IEnumerable<string> ScenarioListJSBundle = new[]
        {
            "ScenarioUI.Resources.scripts.scenario-list.js"
        };

        public static IEnumerable<string> ScenarioCreatorJSBundle = new[]
        {
            "ScenarioUI.Resources.scripts.scenario-creator.js"
        };

        public const string JavaScriptMediaType = "text/javascript";
        public const string CSSMediaType = "text/css";
    }
}
