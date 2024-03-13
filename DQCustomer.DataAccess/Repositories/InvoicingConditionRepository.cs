using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DapperExtensions;
using DQCustomer.BusinessObject;
using DQCustomer.DataAccess.Interfaces;

namespace DQCustomer.DataAccess.Repositories
{
    public class InvoicingConditionRepository : Repository<CpInvoicingCondition>, IInvoicingConditionRepository
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private string _sql;
        public InvoicingConditionRepository(IDbTransaction transaction, IDapperContext context) : base(transaction, context)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public CpInvoicingCondition GetInvoicingConditionById(long Id)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CpInvoicingCondition>(c => c.IConditionID, Operator.Eq, Id));
            return _context.db.GetList<CpInvoicingCondition>(pg).FirstOrDefault();
        }
        public List<CpInvoicingCondition> GetInvoicingConditionByCustomerID(long customerID)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CpInvoicingCondition>(c => c.CustomerID, Operator.Eq, customerID));
            return _context.db.GetList<CpInvoicingCondition>(pg).ToList();
        }
    }
}
