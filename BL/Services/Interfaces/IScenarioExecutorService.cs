using System.Collections.Generic;
using BL.ViewModels;

namespace BL.Services.Interfaces
{
    public interface IScenarioExecutorService
    {
        void RunSingle(int scenarioId);
        List<int> RunAll();
        ExecutionResultViewModel GetExecutionResult(int scenarioId);
        IEnumerable<ExecutionResultViewModel> GetAllExecutionResults(ICollection<int> scenarioIds);
        void FillExecutionResults(ScenarioListViewModel scenarioList);
    }
}