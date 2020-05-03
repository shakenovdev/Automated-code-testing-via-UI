using DAL.Enums;

namespace BL.ViewModels.Internals
{
    public class ActionViewModel
    {
        public int Id { get; set; }
        public ActionType Type { get; set; }
        public short Order { get; set; }
        public VariableViewModel Variable { get; set; }
        public MethodViewModel Method { get; set; }
        public AssertViewModel Assert { get; set; }
    }
}
