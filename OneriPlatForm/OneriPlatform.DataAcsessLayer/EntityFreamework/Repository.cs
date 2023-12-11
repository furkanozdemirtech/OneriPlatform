using OneriPlatform.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace OneriPlatform.DataAcsessLayer.EntityFreamework
{
    public class Repository<T> : RepositortBase, IDataAccess<T> where T : class
    {
        private DbSet<T> _objectSet;
        public Repository()
        {
            _objectSet = context.Set<T>();
        }
        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            return Save();
        }
        public List<T> List()
        {
            return _objectSet.ToList();
        }
        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.ToList();
        }

        public IQueryable<T> ListQuery()
        {
            return _objectSet.AsQueryable<T>();
        }
        List<string> data = new List<string>();
        public int Save()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    data.Add(innerException.Message);
                    innerException = innerException.InnerException;
                }

            }
            return data.Count;
        }

        public int Update(T obj)
        {
            return Save();
        }
    }
}
