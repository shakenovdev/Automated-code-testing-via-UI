namespace BL.ExecutorActions.Interfaces
{
    public interface IExecutor
    {
        ExecutionResult Execute(int scenarioId);
    }

    public class ExecutionResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}