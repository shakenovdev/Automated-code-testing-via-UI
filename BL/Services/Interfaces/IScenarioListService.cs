using BL.ViewModels;
using BL.ViewModels.Internals;

namespace BL.Services.Interfaces
{
    public interface IScenarioListService
    {
        ScenarioListViewModel GetAll();
        void DeleteScenario(int scenarioId);
        void MoveScenarioToFolder(int scenarioId, int folderId);
        FolderViewModel CreateFolder(FolderViewModel folder);
        void RenameFolder(FolderViewModel folder);
        void DeleteFolder(int folderId);
    }
}