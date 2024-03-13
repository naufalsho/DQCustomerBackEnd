using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        long AddReturnID(T entity);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int GetLastID();
        List<T> GetAll();
        List<T> FindAll(Expression<Func<T, bool>> predicate);
        List<T> GetListWithPaging(int pageIndex, int pageSize, out int totalResult);

    }
}
