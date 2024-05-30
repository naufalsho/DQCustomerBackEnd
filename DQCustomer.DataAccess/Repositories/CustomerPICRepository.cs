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
    public class CustomerPICRepository : Repository<CustomerPIC>, ICustomerPICRepository
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private string _sql;
        public CustomerPICRepository(IDbTransaction transaction, IDapperContext context) : base(transaction, context)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public bool InsertCustomerPIC(CustomerPIC objEntity) {
            return true;
        }

        public CustomerPIC GetCustomerPICById(long Id)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CustomerPIC>(c => c.CustomerPICID, Operator.Eq, Id));
            return _context.db.GetList<CustomerPIC>(pg).FirstOrDefault();
        }

        public List<CustomerPIC> GetCustomerPICByCustomerGenId(long customerGenId)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CustomerPIC>(c => c.CustomerGenID, Operator.Eq, customerGenId));
            return _context.db.GetList<CustomerPIC>(pg).ToList();
        }
        public List<Req_CustomerPICGetByCustomerIDMoreDetails> GetCustomerPICByCustomerId(long customerID)
        {
            _sql = "[cp].[spGetCustomerPICByCustomerIdMoreDetails]";
            var vParams = new DynamicParameters();
            vParams.Add("@CustomerID", customerID);
            var output = _context.db.Query<Req_CustomerPICGetByCustomerIDMoreDetails>(_sql, param: vParams, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).ToList();
            return output;
        }
    }
}