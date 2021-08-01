using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Abstract;
using InstaAutoPost.UI.Data.Repository.Abstract;
using InstaAutoPost.UI.Data.Repository.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Data.UnitOfWork.Concrete
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly RSSContextEF _context;
        public EFUnitOfWork(RSSContextEF context)
        {
            if (context == null)
                throw new ArgumentNullException("Context boş olamaz");
            _context = context;
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _context.Dispose();
            }
            this.disposed = true;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase, IEntity, new()
        {
            return new EFRepository<TEntity>(_context);
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
