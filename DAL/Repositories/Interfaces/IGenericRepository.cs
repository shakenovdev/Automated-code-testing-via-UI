using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : ScenarioModel
    {
        void Create(TEntity item);
        TEntity FindById(int id);
        IQueryable<TEntity> Query();
        void SoftRemove(TEntity item);
        void SoftRemoveById(int id);
        void HardRemove(TEntity item);
        void HardRemoveById(int id);
        void Restore(TEntity item);
        void RestoreById(int id);
        void Update(TEntity item);
    }
}