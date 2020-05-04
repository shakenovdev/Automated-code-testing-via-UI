using System.Linq;
using BL.Mappers;
using BL.Services.Interfaces;
using BL.ViewModels;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BL.Services
{
    public class ScenarioCreatorService : IScenarioCreatorService
    {
        private readonly IScenarioRepository _scenarioRepository;

        public ScenarioCreatorService(IScenarioRepository scenarioRepository)
        {
            _scenarioRepository = scenarioRepository;
        }

        public ScenarioCreationViewModel Get(int scenarioId)
        {
            var scenarioEntity = _scenarioRepository.FindById(scenarioId);

            return new ScenarioCreationViewModel
            {
                Id = scenarioEntity.Id,
                Name = scenarioEntity.Name,
                Actions = scenarioEntity.Actions
                    .Select(ActionMapper.ToActionViewModel)
                    .ToList()
            };
        }

        public void Create(ScenarioCreationViewModel scenarioCreation)
        {
            var scenarioEntity = scenarioCreation.ToScenarioCreation();
            _scenarioRepository.Create(scenarioEntity);
        }

        public void Edit(ScenarioCreationViewModel scenarioCreation)
        {
            var scenarioEntity = scenarioCreation.ToScenarioCreation();
            
            _scenarioRepository.UpdateByLocal(scenarioEntity);
        }
    }
}