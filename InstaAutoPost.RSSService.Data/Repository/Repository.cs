using InstaAutoPost.RSSService.Data.Context.MSSQL;
using InstaAutoPost.RSSService.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InstaAutoPost.RSSService.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase, IEntity, new()
    {
        protected RssServiceEFContext _context;
        public bool Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable Get(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
