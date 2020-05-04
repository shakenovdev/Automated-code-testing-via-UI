using ScenarioEntity = DAL.Models.Scenario;

namespace BL.ExecutorActions.Interfaces
{
    public interface IExecutor
    {
        void Execute(ScenarioEntity scenario);
    }
}