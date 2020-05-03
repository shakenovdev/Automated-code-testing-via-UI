using BL.ViewModels;

namespace BL.Services.Interfaces
{
    public interface IScenarioCreatorService
    {
        ScenarioCreationViewModel Get(int scenarioId);
        void Create(ScenarioCreationViewModel scenarioCreation);
        void Edit(ScenarioCreationViewModel scenarioCreation);
    }
}