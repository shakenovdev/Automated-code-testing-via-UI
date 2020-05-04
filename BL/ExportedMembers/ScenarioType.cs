using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests")]
namespace BL.ExportedMembers
{
    public class ScenarioType : BaseMember
    {
        private readonly TypeInfo _typeInfo;

        public ScenarioType(TypeInfo typeInfo) 
            : base(typeInfo)
        {
            _typeInfo = typeInfo;
        }

        public override string FullName => _typeInfo.FullName;
        public override string Type => _typeInfo.Name;

        public Constructor[] Constructors;
        public Field[] Fields;
        public Property[] Properties;
        public Method[] Methods;

        public TypeInstance ConstructObject(object[] parameters)
        {
            return new TypeInstance(this, parameters);
        }
    }
}