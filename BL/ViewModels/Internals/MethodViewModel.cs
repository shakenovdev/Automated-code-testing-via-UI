using System.Collections.Generic;

namespace BL.ViewModels.Internals
{
    public class MethodViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStatic { get; set; }
        public bool IsConstructor { get; set; }
        public VariableViewModel Variable { get; set; }
        public List<ArgumentViewModel> Arguments { get; set; }
    }
}