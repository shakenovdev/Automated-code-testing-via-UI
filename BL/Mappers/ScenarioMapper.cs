using BL.ViewModels.Internals;
using ScenarioEntity = DAL.Models.Scenario;

namespace BL.Mappers
{
    internal static class ScenarioMapper
    {
        public static ScenarioViewModel ToScenarioViewModel(ScenarioEntity entity)
        {
            return new ScenarioViewModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static ScenarioEntity ToScenario(ScenarioViewModel model)
        {
            return new ScenarioEntity
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}