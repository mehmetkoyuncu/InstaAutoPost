using InstaAutoPost.UI.Data.Entities.Abstract;
using InstaAutoPost.UI.Data.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.UnitOfWork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase, IEntity, new();
        int SaveChanges();
    }
}
