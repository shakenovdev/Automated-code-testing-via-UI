using DAL.Enums;

namespace BL.ViewModels.Internals
{
    public class ScenarioViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ExecutionStatus LastExecutedStatus { get; set; }
        public string LastExecutionTime { get; set; }
        public string LastExecutionStackTrace { get; set; }

        internal void FillExecutionResults(ExecutionResultViewModel executionResult)
        {
            if (executionResult.IsSuccess)
            {
                LastExecutedStatus = ExecutionStatus.Success;
                LastExecutionTime = executionResult.ExecutionTime;
            }
            else
            {
                LastExecutedStatus = ExecutionStatus.Fail;
                LastExecutionTime = executionResult.ExecutionTime;
                LastExecutionStackTrace = executionResult.StackTrace;
            }
        }
    }
}