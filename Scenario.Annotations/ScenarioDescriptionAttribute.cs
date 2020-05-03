using System;

namespace Scenario.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property)]
    public class ScenarioDescriptionAttribute : Attribute
    {
        public ScenarioDescriptionAttribute()
        {
        }

        public ScenarioDescriptionAttribute(string title)
        {
            Title = title;
        }

        public ScenarioDescriptionAttribute(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}
