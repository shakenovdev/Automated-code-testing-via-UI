using System.Reflection;

namespace BL.ExportedMembers
{
    public class Constructor : BaseMethod
    {
        private readonly ConstructorInfo _constructorInfo;

        public Constructor(ConstructorInfo constructorInfo)
            : base(constructorInfo)
        {
            _constructorInfo = constructorInfo;
        }

        public object Construct(object[] parameters)
        {
            return _constructorInfo.Invoke(parameters);
        }

        public override string Type => null;
    }
}