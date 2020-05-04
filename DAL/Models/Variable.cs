namespace DAL.Models
{
    public class Variable : ScenarioModel
    {
        public int? ParentVariableId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string PropertyName { get; set; }
        public string ConstantValue { get; set; }

        public virtual Variable ParentVariable { get; set; }
    }
}