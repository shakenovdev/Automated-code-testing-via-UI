using System.Reflection;

namespace BL.ExportedMembers
{
    public class Field : BaseMember
    {
        private readonly FieldInfo _fieldInfo;

        public Field(FieldInfo fieldInfo) 
            : base(fieldInfo)
        {
            _fieldInfo = fieldInfo;
        }

        public object GetValue(object obj)
        {
            return _fieldInfo.GetValue(obj);
        }

        public void SetValue(object obj, object value)
        {
            _fieldInfo.SetValue(obj, value);
        }

        public override string Type => _fieldInfo.FieldType.Name;
    }
}