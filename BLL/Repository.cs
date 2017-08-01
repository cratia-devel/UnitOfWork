using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.Repository
{
    public class Repository<TContext, TEntity> : IDisposable, IRepository<TEntity> 
        where TEntity : EntityBase
        where TContext : DbContext, new()
    {
        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Context.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        private readonly TContext Context = null;
        private readonly DbSet<TEntity> EntitySet = null;

        public Repository()
        {
            this.Context = new TContext();
            this.EntitySet = this.Context.Set<TEntity>();
        }
        public Repository(TContext _DbContext)
        {
            this.Context = _DbContext;
            this.EntitySet = this.Context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = this.EntitySet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
                return this.EntitySet.Where(filter).ToList();
            else
                return this.EntitySet.ToList();
        }

        public virtual bool Add(TEntity _object)
        {
            if ((_object is TEntity) && (_object != null))
            {
                EntitySet.Add(_object);
                return true;
            }
            return false;
        }
        public virtual bool Add(List<TEntity> _objects)
        {
            if ((_objects is List<TEntity>) && (_objects != null))
            {
                EntitySet.AddRange(_objects);
                return true;
            }
            return false;
        }

        public virtual TEntity Find(int _id)
        {
            if (_id < 0)
                return null;
            return this.EntitySet.FirstOrDefault(x => x.Id == _id);
        }
        public virtual TEntity Find(Expression<Func<TEntity, bool>> filter)
        {
            if (filter != null)
                return this.EntitySet.FirstOrDefault(filter);
            return null;
        }
        public virtual TEntity Firts()
        {
            return this.EntitySet.FirstOrDefault();
        }
        public virtual TEntity Last()
        {
            return this.EntitySet.LastOrDefault();
        }

        public virtual bool Delete(int _id)
        {
            if (_id < 0)
                return false;
            TEntity _data = this.EntitySet.Find(_id);
            if (_data == null)
                return false;
            this.EntitySet.Remove(_data);
            return true;
        }
        public virtual bool Delete(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
                return false;
            var data = this.EntitySet.Where(filter);
            if (data.Count() == 0)
            {
                return false;
            }
            this.EntitySet.RemoveRange(data);
            return true;
        }

        public virtual bool Update(TEntity _objectupdate)
        {
            if (_objectupdate == null)
            {
                return false;
            }
            _objectupdate.Updated_At = DateTime.Now;
            this.EntitySet.Attach(_objectupdate);
            this.Context.Entry(_objectupdate).State = EntityState.Modified;
            return true;
        }
    }
}