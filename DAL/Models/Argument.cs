namespace DAL.Models
{
    public class Argument : ScenarioModel
    {
        public int MethodId { get; set; }
        public int VariableId { get; set; }
        public int Order { get; set; }

        public virtual Variable Variable { get; set; }
    }
}