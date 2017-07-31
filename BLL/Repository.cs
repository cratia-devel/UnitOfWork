using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.Repository
{
    public class Repository<T> : IDisposable, IRepository<T> 
        where T : EntityBase
    {
        private DbContext Context;
        private DbSet<T> EntitySet;

        public Repository()
        {
            this.Context = new SystemContext();
            this.EntitySet = this.Context.Set<T>();
        }

        public Repository(DbContext _DbContext)
        {
            this.Context = _DbContext;
            this.EntitySet = this.Context.Set<T>();
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Context.Dispose();
                    // TODO: elimine el estado administrado (objetos administrados).
                }

                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public virtual IEnumerable<T> GetAll()
        {
            return this.EntitySet.ToList();
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> p)
        {
            return this.EntitySet.Where(p).ToList();
        }

        public virtual bool Add(T _object)
        {
            if ((_object is T) && (_object != null))
            {
                try
                {
                    EntitySet.Add(_object);
                    int count = Context.SaveChanges();
                    if (count > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    this.Context.Entry(_object).State = EntityState.Detached;
                    Console.WriteLine("Context.Entry({0}).State = EntityState.Detached", _object);
                    Console.WriteLine("[ERROR] -- Exception: " + ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
            return false;
        }
        public virtual bool Add(List<T> _objects)
        {
            if ((_objects is List<T>) && (_objects != null))
            {
                try
                {
                    EntitySet.AddRange(_objects);
                    int count = Context.SaveChanges();
                    if (count > 0)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    foreach (var item in _objects)
                    {
                        this.Context.Entry(item).State = EntityState.Detached;
                        Console.WriteLine("Context.Entry({0}).State = EntityState.Detached", item);
                    }
                    Console.WriteLine("[ERROR] -- Exception: " + ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
            return false;
        }
        public virtual T Find(int _id)
        {
            return this.EntitySet.FirstOrDefault(x => x.Id == _id);
        }
        public virtual T Find(Expression<Func<T, bool>> p)
        {
            return this.EntitySet.FirstOrDefault(p);
        }
        public virtual T Firts()
        {
            return this.EntitySet.FirstOrDefault();
        }
        public virtual T Last()
        {
            return this.EntitySet.LastOrDefault();
        }
        public virtual bool Delete(int _id)
        {
            try
            {
                if (_id < 0)
                    return false;
                T _data = this.EntitySet.Find(_id);
                if (_data == null)
                    return false;
                this.EntitySet.Remove(_data);
                int count = this.Context.SaveChanges();
                if (count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] -- Exception: " + ex.Message);
                Console.WriteLine(ex.InnerException.Message);
            }
            return false;
        }
        public virtual bool Delete(Expression<Func<T, bool>> p)
        {
            try
            {
                if (p == null)
                    return false;
                var data = this.EntitySet.Where(p);
                if (data.Count() == 0)
                {
                    return false;
                }
                this.EntitySet.RemoveRange(data);
                int count = this.Context.SaveChanges();
                if (count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] -- Exception: " + ex.Message);
                Console.WriteLine(ex.InnerException.Message);
            }
            return false;
        }
    }
}