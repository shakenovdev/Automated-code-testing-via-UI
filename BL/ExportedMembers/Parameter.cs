using System.Reflection;

namespace BL.ExportedMembers
{
    public class Parameter
    {
        private readonly ParameterInfo _parameterInfo;

        public Parameter(ParameterInfo parameterInfo)
        {
            _parameterInfo = parameterInfo;
        }

        public string Name => _parameterInfo.Name;

        public string Type => _parameterInfo.ParameterType.Name;

        public int Position => _parameterInfo.Position;
    }
}
