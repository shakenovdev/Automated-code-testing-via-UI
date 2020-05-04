using System.Linq;
using BL.ViewModels;
using ScenarioEntity = DAL.Models.Scenario;

namespace BL.Mappers
{
    internal static class ScenarioCreationMapper
    {
        public static ScenarioCreationViewModel ToScenarioCreationViewModel(ScenarioEntity entity)
        {
            return new ScenarioCreationViewModel
            {
                Id = entity.Id,
                FolderId = entity.FolderId,
                Name = entity.Name,
                Actions = entity.Actions
                    .Select(ActionMapper.ToActionViewModel)
                    .ToList()
            };
        }

        public static ScenarioEntity ToScenarioCreation(this ScenarioCreationViewModel model)
        {
            return new ScenarioEntity()
            {
                Id = model.Id,
                FolderId = model.FolderId,
                Name = model.Name,
                Actions = model.Actions
                    .Select(x => ActionMapper.ToAction(x, model.Id))
                    .ToList()
            };
        }
    }
}