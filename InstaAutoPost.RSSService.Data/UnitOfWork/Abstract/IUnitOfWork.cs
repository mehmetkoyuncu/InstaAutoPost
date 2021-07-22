using InstaAutoPost.RSSService.Data.Entities.Abstract;
using InstaAutoPost.RSSService.Data.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.RSSService.Data.UnitOfWork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase, IEntity, new();
        int SaveChanges();
    }
}
