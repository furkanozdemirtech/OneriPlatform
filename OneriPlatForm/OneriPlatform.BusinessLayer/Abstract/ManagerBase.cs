using OneriPlatform.Core.DataAccess;
using OneriPlatform.DataAcsessLayer.EntityFreamework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OneriPlatform.BusinessLayer.Abstract
{
    public abstract class ManagerBase<T> : IDataAccess<T> where T : class
    {
        public Repository<T> repo = new Repository<T>();
        public virtual int Delete(T obj)
        {
            return repo.Delete(obj);
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return repo.Find(where);
        }

        public int Insert(T obj)
        {
            return repo.Insert(obj);
        }

        public List<T> List()
        {
            return repo.List();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return repo.List(where);
        }

        public IQueryable<T> ListQuery()
        {
            return repo.ListQuery();
        }

        public int Save()
        {
            return repo.Save();
        }

        public int Update(T obj)
        {
            return repo.Update(obj);
        }
    }
}
