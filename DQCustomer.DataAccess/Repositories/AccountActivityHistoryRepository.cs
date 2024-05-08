using Dapper;
using DapperExtensions;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using DQCustomer.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DQCustomer.DataAccess.Repositories
{
    public class AccountHistoryActivityRepository : Repository<CpAccountActivityHistory>, IAccountActivityHistoryRepository
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private string _sql;
        public AccountHistoryActivityRepository(IDbTransaction transaction, IDapperContext context) : base(transaction, context)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public CpAccountActivityHistory GetByID(long accountHistoryActivityID)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CpAccountActivityHistory>(c => c.AccountActivityHistoryID, Operator.Eq, accountHistoryActivityID));
            return _context.db.GetList<CpAccountActivityHistory>(pg).FirstOrDefault();
        }

        public bool DeleteByID(long accountActivityHistoryID)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CpAccountActivityHistory>(c => c.AccountActivityHistoryID, Operator.Eq, accountActivityHistoryID));
            bool output = _context.db.Delete<CpAccountActivityHistory>(pg, _transaction);
            return output;
        }

    }
}