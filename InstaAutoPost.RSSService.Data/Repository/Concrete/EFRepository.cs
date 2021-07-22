using InstaAutoPost.RSSService.Data.Context;
using InstaAutoPost.RSSService.Data.Entities.Abstract;
using InstaAutoPost.RSSService.Data.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InstaAutoPost.RSSService.Data.Repository.Concrete
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

        public bool Add(TEntity entity)
        {
            bool _control = false;
            try
            {
                _dbset.Add(entity);
                _control = true;
            }
            catch (Exception)
            {
                _control = false;
            }
            return _control;
        }

        public IQueryable Get(Expression<Func<TEntity, bool>> filter)
        {
            return _dbset.Where(filter);
        }

        public bool Remove(TEntity entity)
        {
            bool _control = false;
            if (entity.GetType().GetProperty("IsDeleted") != null)
            {
                try
                {
                    entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
                    this.Update(entity);
                    _control = true;

                }
                catch
                {
                    _control = false;
                }
            }
            else
            {
                try
                {
                    if(_context.Entry(entity).State!=EntityState.Deleted)
                        _context.Entry(entity).State = EntityState.Deleted;
                    else
                    {
                        _dbset.Attach(entity);
                        _dbset.Remove(entity);
                    }
                    _control = true;
                }
                catch 
                {
                    _control = false;
                }
            }
            return _control;
        }

        public bool Update(TEntity entity)
        {
            bool _control = false;
            try
            {
                _dbset.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _control = true;
            }
            catch
            {
                _control = false;
            }
            return _control;
        }
    }
}
