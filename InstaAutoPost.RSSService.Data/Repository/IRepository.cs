using InstaAutoPost.RSSService.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InstaAutoPost.RSSService.Data.Repository
{
    public interface IRepository<TEntity>
        where TEntity : EntityBase, IEntity, new()
    {
        bool Add(TEntity entity);
        bool Remove(TEntity entity);
        bool Update(TEntity entity);
        IQueryable Get(Expression<Func<TEntity, bool>> filter);
    }
}
