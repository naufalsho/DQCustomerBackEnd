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
    public class SalesHistoryRepository : Repository<CpSalesHistory>, ISalesHistoryRepository
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private string _sql;
        public SalesHistoryRepository(IDbTransaction transaction, IDapperContext context) : base(transaction, context)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public CpSalesHistory GetSalesHistoryByCustomerID(long SalesHistoryID)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CpSalesHistory>(c => c.SalesHistoryID, Operator.Eq, SalesHistoryID));
            return _context.db.GetList<CpSalesHistory>(pg).FirstOrDefault();
        }
        public List<Req_CustomerSettingShareableApprovalStatus_ViewModel> GetShareableStatus(long customerID)
        {
            _sql = "[cp].[spGetSalesHistoryApprovalInfo]";
            var vParams = new DynamicParameters();
            vParams.Add("@CustomerID", customerID);
            var output = _context.db.Query<Req_CustomerSettingShareableApprovalStatus_ViewModel>(
                _sql,
                param: vParams,
                transaction: _transaction,
                buffered: false,
                commandTimeout: null,
                commandType: CommandType.StoredProcedure
            ).ToList();

            return output;
        }
        public List<Req_CustomerSettingGetAccountOwner_ViewModel> GetAccountOwner(long customerID)
        {
            _sql = "[cp].[spGetAccountOwner]";
            var vParams = new DynamicParameters();
            vParams.Add("@CustomerID", customerID);
            var output = _context.db.Query<Req_CustomerSettingGetAccountOwner_ViewModel>(
                _sql,
                param: vParams,
                transaction: _transaction,
                buffered: false,
                commandTimeout: null,
                commandType: CommandType.StoredProcedure
            ).ToList();
            return output;
        }

        public List<Req_CustomerSettingGetSalesAssignHistory_ViewModel> GetSalesAssignHistory(long customerID)
        {
            _sql = "[cp].[spGetSalesAssignHistory]";
            var vParams = new DynamicParameters();
            vParams.Add("@CustomerID", customerID);
            var output = _context.db.Query<Req_CustomerSettingGetSalesAssignHistory_ViewModel>(
                _sql,
                param: vParams,
                transaction: _transaction,
                buffered: false,
                commandTimeout: null,
                commandType: CommandType.StoredProcedure
            ).ToList();
            return output;
        }

        public List<Req_CustomerSettingGetSalesHistoryByCustID_ViewModel> GetSalesHistoryByID(long customerID)
        {
            _sql = "[cp].[spGetSalesHistoryByCustomerID]";
            var vParams = new DynamicParameters();
            vParams.Add("@CustomerID", customerID);
            var output = _context.db.Query<Req_CustomerSettingGetSalesHistoryByCustID_ViewModel>(_sql, param: vParams, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).ToList();
            return output;
        }

    }
}
