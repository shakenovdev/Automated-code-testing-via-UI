using DAL.Enums;

namespace DAL.Models
{
    public class Action : ScenarioModel
    {
        public int ScenarioId { get; set; }
        public int? VariableId { get; set; }
        public int? MethodId { get; set; }
        public int? AssertId { get; set; }
        public ActionType Type { get; set; }
        public short Order { get; set; }
        
        public virtual Variable Variable { get; set; }
        public virtual Method Method { get; set; }
        public virtual Assert Assert { get; set; }
    }
}