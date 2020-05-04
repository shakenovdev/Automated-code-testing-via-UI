namespace BL.ViewModels
{
    public class ExecutionResultViewModel
    {
        public int ScenarioId { get; set; }
        public bool IsSuccess { get; set; }
        public string ExecutionTime { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}