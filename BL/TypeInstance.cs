using BL.ExportedMembers;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
namespace BL
{
    public class TypeInstance
    {
        private readonly ScenarioType _scenarioType;
        private readonly object _typeInstance;

        public TypeInstance(ScenarioType scenarioType, object[] parameters)
        {
            _scenarioType = scenarioType;
            _typeInstance = Construct(parameters);
        }

        public void SetProperty(string name, object value)
        {
            var property = _scenarioType.Properties.First(x => x.FullName == name);
            property.SetValue(_typeInstance, value);
        }

        public object GetProperty(string name)
        {
            var property = _scenarioType.Properties.First(x => x.FullName == name);
            return property.GetValue(_typeInstance);
        }

        public void SetField(string name, object value)
        {
            var field = _scenarioType.Fields.First(x => x.FullName == name);
            field.SetValue(_typeInstance, value);
        }

        public object GetField(string name)
        {
            var field = _scenarioType.Fields.First(x => x.FullName == name);
            return field.GetValue(_typeInstance);
        }

        public object InvokeMethod(string name, object[] parameters)
        {
            var method = _scenarioType.Methods.First(x => x.FullName == name);
            return method.Invoke(_typeInstance, parameters);
        }

        private object Construct(object[] parameters)
        {
            var constructor = _scenarioType.Constructors.Single(x => x.ParametersCount == parameters.Length);
            return constructor.Construct(parameters);
        }
    }
}
