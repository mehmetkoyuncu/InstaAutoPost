using InstaAutoPost.RSSService.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InstaAutoPost.RSSService.Data.Repository.Abstract
{
    public interface IRepository<TEntity>
       where TEntity : EntityBase, IEntity, new()
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);
    }
}
