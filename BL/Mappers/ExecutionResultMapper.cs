using BL.ExecutorActions.Interfaces;
using BL.ViewModels;

namespace BL.Mappers
{
    internal static class ExecutionResultMapper
    {
        public static ExecutionResultViewModel ToExecutionResultViewModel(this ExecutionResult executionResult)
        {
            return new ExecutionResultViewModel
            {
                IsSuccess = executionResult.IsSuccess,
                Message = executionResult.Message
            };
        }
    }
}