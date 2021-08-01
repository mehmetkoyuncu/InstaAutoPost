using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Abstract;
using InstaAutoPost.UI.Data.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InstaAutoPost.UI.Data.Repository.Concrete
{
    public class EFRepository<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase, IEntity, new()
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbset;

        public EFRepository(RSSContextEF context)
        {
            if (context == null)
                throw new ArgumentNullException("Context boş olamaz.");

            _context = context;
            _dbset = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbset.Add(entity);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return _dbset.Where(filter);
        }

        public void Remove(TEntity entity)
        {
            if (entity.GetType().GetProperty("IsDeleted") != null)
            {
                entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
                this.Update(entity);
            }
            else
            {
                if (_context.Entry(entity).State != EntityState.Deleted)
                    _context.Entry(entity).State = EntityState.Deleted;
                else
                {
                    _dbset.Attach(entity);
                    _dbset.Remove(entity);
                }
            }
        }

        public void Update(TEntity entity)
        {
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
