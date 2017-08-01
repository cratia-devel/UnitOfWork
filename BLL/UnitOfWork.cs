using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace BLL.Repository
{
    class UnitOfWork<TContext> : IDisposable
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
        private readonly Dictionary<Type, object> Repositories = null;

        public UnitOfWork()
        {
            this.Context = new TContext();
            this.Repositories = new Dictionary<Type, object>();
        }

        public virtual IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : EntityBase
        {
            if (!Repositories.Keys.Contains(typeof(TEntity)))
            {
                this.Repositories.Add(
                    typeof(TEntity),
                    new Repository<TContext, TEntity>(this.Context)
                    );
            }
            return Repositories[typeof(TEntity)] as IRepository<TEntity>; ;
        }

        private int Save()
        {
            bool saveFailed;
            int count = 0;
            do
            {
                saveFailed = false;
                try
                {
                    count += this.Context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    saveFailed = true;
                    var _entry = Context.ChangeTracker.Entries().First();
                    this.RollBack(_entry);
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
                }
                catch (Exception ex)
                {
                    saveFailed = true;
                    this.RollBack();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
                }
            } while (saveFailed);
            return count;
        }

        private void RollBack(EntityEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Modified:
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                default:
                    break;
            }
        }

        public IEnumerable<object> RollBack()
        {
            List<EntityEntry> _data = Context.ChangeTracker.Entries()
                .Where(x => (x.State != EntityState.Unchanged) || (x.State != EntityState.Detached))
                .ToList();
            foreach (var entry in _data)
            {
                this.RollBack(entry);
            }
            List<object> _dataObject = new List<object>();
            foreach (var item in _data)
            {
                _dataObject.Add(item.CurrentValues.ToObject());
            }
            return _dataObject;
        }

        public virtual int Commit()
        {
            return this.Save();
        }
    }
}