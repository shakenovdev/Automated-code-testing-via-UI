using System.Reflection;
using Scenario.Annotations;

namespace BL.ExportedMembers
{
    public abstract class BaseMember
    {
        private readonly MemberInfo _memberInfo;

        protected BaseMember(MemberInfo memberInfo)
        {
            _memberInfo = memberInfo;
            var attribute = _memberInfo.GetCustomAttribute<ScenarioDescriptionAttribute>();

            if (attribute != null)
            {
                Title = string.IsNullOrEmpty(attribute.Title) ? _memberInfo.Name : attribute.Title;
                Description = attribute.Description;
            }
        }

        public string Title;
        public string Description;
        public virtual string FullName => _memberInfo.Name;
        public abstract string Type { get; }
    }
}