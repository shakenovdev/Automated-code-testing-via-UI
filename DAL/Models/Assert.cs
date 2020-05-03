using DAL.Enums;

namespace DAL.Models
{
    public class Assert : ScenarioModel
    {
        public AssertType Type { get; set; }
        public int ValueVariableId { get; set; }
        public int? ExpectedVariableId { get; set; }
        public int? DeltaVariableId { get; set; }
        
        public virtual Variable ValueVariable { get; set; }
        public virtual Variable ExpectedVariable { get; set; }
        public virtual Variable DeltaVariable { get; set; }
    }
}