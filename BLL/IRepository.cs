using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Models;

namespace BLL.Repository
{
    public interface IRepository<T> 
        where T : EntityBase
    {
        bool Add(List<T> _objects);
        bool Add(T _object);
        bool Delete(Expression<Func<T, bool>> p);
        bool Delete(int _id);
        T Find(Expression<Func<T, bool>> p);
        T Find(int _id);
        T Firts();
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> p);
        T Last();
    }
}