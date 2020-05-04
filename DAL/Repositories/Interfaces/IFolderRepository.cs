using System.Collections.Generic;
using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IFolderRepository : IGenericRepository<Folder>
    {
        List<Scenario> DeleteFolder(int folderId);
    }
}