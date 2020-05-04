using DAL.Enums;

namespace BL.ViewModels.Internals
{
    public class AssertViewModel
    {
        public int Id { get; set; }
        public AssertType Type { get; set; }
        public string ExceptionMessage { get; set; }
        public VariableViewModel ValueVariable { get; set; }
        public VariableViewModel ExpectedVariable { get; set; }
        public VariableViewModel DeltaVariable { get; set; }
    }
}