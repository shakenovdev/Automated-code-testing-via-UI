using System.Linq;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Utilities;

namespace DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : ScenarioModel
    {
        protected readonly ScenarioContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(ScenarioContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public void Create(TEntity item)
        {
            DbSet.Add(item);
            Update(item);
        }

        public TEntity FindById(int id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<TEntity> Query()
        {
            return DbSet;
        }

        public void SoftRemove(TEntity item)
        {
            if (item.IsDeleted)
            {
                HardRemove(item);
            }
            else
            {
                item.IsDeleted = true;
                Update(item);
            }
        }

        public void SoftRemoveById(int id)
        {
            var item = FindById(id);
            SoftRemove(item);
        }

        public void HardRemove(TEntity item)
        {
            DbSet.Remove(item);
            Context.SaveChanges();
        }

        public void HardRemoveById(int id)
        {
            var item = FindById(id);
            HardRemove(item);
        }

        public void Restore(TEntity item)
        {
            item.IsDeleted = false;
            Update(item);
        }

        public void RestoreById(int id)
        {
            var item = FindById(id);
            Restore(item);
        }

        public void Update(TEntity item)
        {
            DbSet.Update(item);
            Context.SaveChanges();
        }
    }
}