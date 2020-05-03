using System.Linq;
using System.Reflection;

namespace BL.ExportedMembers
{
    public abstract class BaseMethod : BaseMember
    {
        private readonly MethodBase _methodBase;

        protected BaseMethod(MethodBase methodBase)
            : base(methodBase)
        {
            _methodBase = methodBase;
            Parameters = _methodBase.GetParameters()
                .Select(x => new Parameter(x))
                .ToArray();
        }

        protected Parameter[] Parameters;

        public override string ToString()
        {
            var orderedParameters = Parameters.OrderBy(x => x.Position);
            return string.Join(", ", orderedParameters);
        }
    }
}
