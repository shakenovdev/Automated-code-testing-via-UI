using System.Collections.Generic;
using BL.ViewModels;

namespace BL.Services.Interfaces
{
    public interface IScenarioExecutorService
    {
        ExecutionResultViewModel RunSingle(int scenarioId);
        IEnumerable<ExecutionResultViewModel> RunAll();
    }
}