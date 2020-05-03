namespace BL.ViewModels
{
    public class ExecutionResultViewModel
    {
        public bool? IsSuccess { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}