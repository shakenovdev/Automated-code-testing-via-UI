using System;

namespace Scenario.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property)]
    public class ScenarioActivationAttribute : Attribute
    {
        public ScenarioActivationAttribute(ActivationMode activationMode)
        {
            ActivationMode = activationMode;
        }

        public ActivationMode ActivationMode { get; set; }
    }
    
    public enum ActivationMode
    {
        None,
        Single, // TODO: rename
        All
    }
}
