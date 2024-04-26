using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using DapperExtensions;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using DQCustomer.DataAccess.Interfaces;

namespace DQCustomer.DataAccess.Repositories
{
    public class RelatedCustomerRepository : Repository<CpRelatedCustomer>, IRelatedCustomerRepository
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private string _sql;
        public RelatedCustomerRepository(IDbTransaction transaction, IDapperContext context) : base(transaction, context)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public CpRelatedCustomer GetRelatedCustomerById(long Id)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CpRelatedCustomer>(c => c.RCustomerID, Operator.Eq, Id));
            return _context.db.GetList<CpRelatedCustomer>(pg).FirstOrDefault();
        }
        public List<Req_CustomerSettingGetRelatedCustomer_ViewModel> GetRelatedCustomerByCustomerID(long customerID)
        {
            _sql = "[cp].[spGetRelatedCustomer]";
            var vParams = new DynamicParameters();
            vParams.Add("@CustomerID", customerID);
            var output = _context.db.Query<Req_CustomerSettingGetRelatedCustomer_ViewModel>(_sql, param: vParams, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).ToList();
            return output;
        }

        public bool DeleteRelatedCustomer(long Id)
        {
            _sql = "[cp].[spDeleteRelatedCustomerById]";
            var vParams = new DynamicParameters();
            vParams.Add("@Id", Id);
            var affectedRows = _context.db.Execute(_sql, param: (object)vParams, transaction: _transaction, commandTimeout: null, commandType: CommandType.StoredProcedure);
            return affectedRows > 0;
        }

        public List<Req_CustomerSettingGetRelatedCustomerMoreDetailByID_ViewModel> GetRelatedCustomerMoreDetailsByID(long customerID, long customerGenID)
        {
            _sql = "[cp].[spGetRelatedCustomerMoreDetailByID]";
            var vParams = new DynamicParameters();
            vParams.Add("@CustomerID", customerID);
            vParams.Add("@CustomerGenID", customerGenID);
            var output = _context.db.Query<Req_CustomerSettingGetRelatedCustomerMoreDetailByID_ViewModel>(_sql, param: vParams, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).ToList();
            return output;
        }
    }
}
