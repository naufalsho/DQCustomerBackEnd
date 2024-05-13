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
    public class AccountActivityHistoryRepository : Repository<CpAccountActivityHistory>, IAccountActivityHistoryRepository
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private string _sql;
        public AccountActivityHistoryRepository(IDbTransaction transaction, IDapperContext context) : base(transaction, context)
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

        public List<Req_AccountActivityHistoryGetByID> GetAccountActivityHistoryByID(long customerID, long customerGenID)
        {
            _sql = "[cp].[spGetAccountActivityHistoryByID]";
            var vParams = new DynamicParameters();
            vParams.Add("@CustomerID", customerID);
            vParams.Add("@CustomerGenID", customerGenID);
            var output = _context.db.Query<Req_AccountActivityHistoryGetByID>(_sql, param: vParams, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).ToList();
            return output;
        }

    }
}