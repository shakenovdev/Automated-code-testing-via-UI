using System.Reflection;

namespace BL.ExportedMembers
{
    public class Method : BaseMethod
    {
        private readonly MethodInfo _methodInfo;

        public Method(MethodInfo methodInfo) 
            : base(methodInfo)
        {
            _methodInfo = methodInfo;
        }

        public object Invoke(object obj, object[] parameters)
        {
            return _methodInfo.Invoke(obj, parameters);
        }

        public override string Type => _methodInfo.ReturnType.Name;
    }
}