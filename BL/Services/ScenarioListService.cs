using BL.Mappers;
using BL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using System.Linq;
using BL.ViewModels;
using BL.ViewModels.Internals;

namespace BL.Services
{
    public class ScenarioListService : IScenarioListService
    {
        private readonly IScenarioRepository _scenarioRepository;
        private readonly IGenericRepository<Folder> _folderRepository;

        public ScenarioListService(IScenarioRepository scenarioRepository,
            IGenericRepository<Folder> folderRepository)
        {
            _scenarioRepository = scenarioRepository;
            _folderRepository = folderRepository;
        }

        public ScenarioListViewModel GetAll()
        {
            var allScenarios = _scenarioRepository.Query()
                .Where(x => !x.IsDeleted)
                .ToList();

            var allFolders = _folderRepository.Query()
                .Where(x => !x.IsDeleted)
                .ToList();

            FolderViewModel AssembleFolder(Folder folder)
            {
                var folderScenarios = allScenarios
                    .Where(x => x.FolderId.GetValueOrDefault() == folder.Id)
                    .Select(ScenarioMapper.ToScenarioViewModel)
                    .ToList();

                var nestedFolders = allFolders
                    .Where(x => x.ParentId.GetValueOrDefault() == folder.Id)
                    .Select(AssembleFolder)
                    .ToList();

                return new FolderViewModel()
                {
                    Id = folder.Id,
                    ParentId = folder.ParentId,
                    Name = folder.Name,
                    Folders = nestedFolders,
                    Scenarios = folderScenarios
                };
            }

            var folders = allFolders
                .Where(x => !x.ParentId.HasValue)
                .Select(AssembleFolder)
                .ToList();

            var rootScenarios = allScenarios
                .Where(x => !x.FolderId.HasValue)
                .Select(ScenarioMapper.ToScenarioViewModel)
                .ToList();

            var trashScenarios = _scenarioRepository.Query()
                .Where(x => x.IsDeleted)
                .AsEnumerable()
                .Select(ScenarioMapper.ToScenarioViewModel)
                .ToList();

            return new ScenarioListViewModel
            {
                Folders = folders,
                RootScenarios = rootScenarios,
                TrashScenarios = trashScenarios
            };
        }

        public void DeleteScenario(int scenarioId)
        {
            _scenarioRepository.SoftRemoveById(scenarioId);
        }

        public void MoveScenarioToFolder(int scenarioId, int folderId)
        {
            var scenarioEntity = _scenarioRepository.FindById(scenarioId);
            scenarioEntity.FolderId = folderId;
            _scenarioRepository.Update(scenarioEntity);
        }

        public FolderViewModel CreateFolder(FolderViewModel folder)
        {
            var folderEntity = new Folder
            {
                ParentId = folder.ParentId,
                Name = folder.Name
            };

            _folderRepository.Create(folderEntity);

            return new FolderViewModel
            {
                Id = folderEntity.Id,
                ParentId = folderEntity.ParentId,
                Name = folderEntity.Name
            };
        }

        public void RenameFolder(FolderViewModel folder)
        {
            var folderEntity = _folderRepository.FindById(folder.Id);
            folderEntity.Name = folder.Name;
            _folderRepository.Update(folderEntity);
        }

        public void DeleteFolder(int folderId)
        {
            _scenarioRepository.SoftDeleteFolderScenarios(folderId);
            _folderRepository.HardRemoveById(folderId);
        }
    }
}