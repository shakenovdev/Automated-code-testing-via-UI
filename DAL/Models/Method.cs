using System.Collections.Generic;

namespace DAL.Models
{
    public class Method : ScenarioModel
    {
        public int? VariableId { get; set; }
        public bool IsStatic { get; set; }
        public bool IsConstructor { get; set; }
        public string Name { get; set; }

        public virtual Variable Variable { get; set; }
        public virtual List<Argument> Arguments { get; set; }
    }
}