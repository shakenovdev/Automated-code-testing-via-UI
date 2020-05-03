using System.Reflection;

namespace BL.ExportedMembers
{
    public class Property : BaseMember
    {
        private readonly PropertyInfo _propertyInfo;

        public Property(PropertyInfo propertyInfo) 
            : base(propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

        public object GetValue(object obj)
        {
            return _propertyInfo.GetValue(obj);
        }

        public void SetValue(object obj, object value)
        {
            _propertyInfo.SetValue(obj, value);
        }

        public override string Type => _propertyInfo.PropertyType.Name;
    }
}