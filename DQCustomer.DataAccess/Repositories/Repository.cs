using DQCustomer.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using DapperExtensions;
using OMS.DataAccess.Mapping;

namespace DQCustomer.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        
        private IDapperContext _context;
        private IDbTransaction _transaction;
        public Repository(IDbTransaction transaction, IDapperContext context)
        {
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(SchemaMapping<>);
            
            this._context = context;
            this._transaction = transaction;
        }

        public void Add(T entity)
        {
            this._context.db.Insert(entity, _transaction);
        }


        public void Update(T entity)
        {
            this._context.db.Update(entity, _transaction);
        }

        public void Delete(T entity)
        {
            this._context.db.Delete(entity, _transaction);
        }

        public List<T> GetAll()
        {
            return this._context.db.GetList<T>().ToList();
        }

        public int GetLastID()
        {
            return this._context.GetLastId();
        }

        public long AddReturnID(T entity)
        {
            return this._context.db.Insert(entity, _transaction);
        }

        public List<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return this._context.db.GetList<T>().AsQueryable().Where(predicate).ToList();
        }

        public List<T> GetListWithPaging(int pageIndex, int pageSize, out int total)
        {
            var query = $"SELECT * FROM {typeof(T).Name} ORDER BY ID OFFSET ((@PageNumber - 1) * @Rows) ROWS FETCH NEXT @Rows ROWS ONLY  ;" +
                        $"SELECT COUNT(*) FROM {typeof(T).Name}";

            List<T> result;

            using (var multi = _context.db.QueryMultiple(query, new { PageNumber = pageIndex, Rows = pageSize }))
            {
                result = multi.Read<T>().ToList();
                total = multi.Read<int>().Single();
            }

            return result;
        }
    }
}
