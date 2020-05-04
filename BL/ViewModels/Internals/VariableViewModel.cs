namespace BL.ViewModels.Internals
{
    public class VariableViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string PropertyName { get; set; }
        public string ConstantValue { get; set; }
        public VariableViewModel ParentVariable { get; set; }
    }
}