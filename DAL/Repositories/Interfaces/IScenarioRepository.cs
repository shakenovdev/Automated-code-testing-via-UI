using System.Collections.Generic;
using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IScenarioRepository : IGenericRepository<Scenario>
    {
        Scenario GetEntirely(int scenarioId);
        void SoftDeleteFolderScenarios(int folderId);
        void UpdateByLocal(Scenario scenario);
    }
}