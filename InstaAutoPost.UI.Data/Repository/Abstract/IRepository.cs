using InstaAutoPost.UI.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InstaAutoPost.UI.Data.Repository.Abstract
{
    public interface IRepository<TEntity>
     where TEntity : EntityBase, IEntity, new()
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);
        void AddList(List<TEntity> entityList);
        List<TEntity> GetAll();
    }
}
