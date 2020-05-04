using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class FolderRepository : GenericRepository<Folder>, IFolderRepository
    {
        public FolderRepository(ScenarioContext context)
            : base(context)
        {
        }

        public List<Scenario> DeleteFolder(int folderId)
        {
            void SoftDeleteScenarios(Folder folder)
            {
                var folderScenarios = Context.Scenarios
                    .Where(x => x.FolderId.GetValueOrDefault() == folder.Id)
                    .ToArray();

                foreach (var scenarioEntity in folderScenarios)
                {
                    scenarioEntity.FolderId = null;
                    scenarioEntity.IsDeleted = true;
                }

                if (folder.Children == null)
                    return;

                foreach (var nestedFolder in folder.Children)
                {
                    SoftDeleteScenarios(nestedFolder);
                }
            }

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var folder = DbSet.Find(folderId);

                    SoftDeleteScenarios(folder);
                    DbSet.Remove(folder);

                    Context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }

            return Context.Scenarios.Where(x => x.IsDeleted).ToList();
        }
    }
}