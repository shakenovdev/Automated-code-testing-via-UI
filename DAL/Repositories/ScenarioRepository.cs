using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ScenarioRepository : GenericRepository<Scenario>, IScenarioRepository
    {
        public ScenarioRepository(ScenarioContext context) 
            : base(context)
        {
        }

        public Scenario GetEntirely(int scenarioId)
        {
            // TODO: how to load parent variable?
            var scenario = DbSet
                .Include(x => x.Actions)
                .ThenInclude(x => x.Variable)
                .Include(x => x.Actions)
                .ThenInclude(x => x.Method.Variable)
                .Include(x => x.Actions)
                .ThenInclude(x => x.Method.Arguments)
                .ThenInclude(x => x.Variable)
                .Include(x => x.Actions)
                .ThenInclude(x => x.Assert.ValueVariable)
                .Include(x => x.Actions)
                .ThenInclude(x => x.Assert.ExpectedVariable)
                .Include(x => x.Actions)
                .ThenInclude(x => x.Assert.DeltaVariable)
                .FirstOrDefault(x => x.Id == scenarioId);

            return scenario;
        }

        public void SoftDeleteFolderScenarios(int folderId)
        {
            var scenarioEntities = DbSet
                .Where(x => x.FolderId.GetValueOrDefault() == folderId)
                .ToList();

            foreach (var scenarioEntity in scenarioEntities)
            {
                scenarioEntity.FolderId = null;
                scenarioEntity.IsDeleted = true;
            }

            Context.SaveChanges();
        }

        public void UpdateByLocal(Scenario scenario)
        {
            var local = Context.Set<Scenario>()
                .Local
                .FirstOrDefault(entry => entry.Id == scenario.Id);

            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }

            Context.Entry(scenario).State = EntityState.Modified;

            Context.SaveChanges();
        }
    }
}