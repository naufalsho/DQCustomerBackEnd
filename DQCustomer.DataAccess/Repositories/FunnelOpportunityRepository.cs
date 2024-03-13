using Dapper;
using DapperExtensions;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.Common;
using DQCustomer.BusinessObject.ViewModel;
using DQCustomer.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DQCustomer.DataAccess.Repositories
{
    public class FunnelOpportunityRepository : Repository<SalesFunnelOpportunity>, IFunnelOpportunityRepository
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private string _sql;
        public FunnelOpportunityRepository(IDbTransaction transaction, IDapperContext context):base(transaction,context)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public List<SalesFunnelOpportunity> CheckFunnelToOpportunity(Req_FunnelCheckToOpportunity_ViewModel obj)
        {
            var brand = obj.BrandID.Split(",").ToList();
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<SalesFunnelOpportunity>(f => f.CustomerGenID, Operator.Eq, obj.CustomerGenID));
            pg.Predicates.Add(Predicates.Field<SalesFunnelOpportunity>(f => f.SalesID, Operator.Eq, obj.SalesID));
            pg.Predicates.Add(Predicates.Field<SalesFunnelOpportunity>(f => f.BrandID, Operator.Eq, brand));
            pg.Predicates.Add(Predicates.Field<SalesFunnelOpportunity>(f => f.FunnelID, Operator.Eq, null));
            pg.Predicates.Add(Predicates.Field<SalesFunnelOpportunity>(f => f.Status, Operator.Eq, "NEW"));
            return _context.db.GetList<SalesFunnelOpportunity>(pg).ToList();
        }

        public List<SalesFunnelOpportunityDashboard> GetListDashboard()
        {
            _sql = "[sales].[spGetListFunnelOpportunity]";
            //var vParams = new DynamicParameters();
            //vParams.Add("@FunnelGenID", funnelGenID);
            var output = _context.db.Query<SalesFunnelOpportunityDashboard>(_sql, param: null, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).ToList();

            return output;
        }
		
		public List<SalesFunnelOpportunityDashboard> GetListDashboardBySalesID(string hirarki)
        {
            _sql = "[sales].[spGetListFunnelOpportunityBySalesID]";
            var vParams = new DynamicParameters();
            vParams.Add("@Hirarki", hirarki);
            var output = _context.db.Query<SalesFunnelOpportunityDashboard>(_sql, param: (object)vParams, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).ToList();

            return output;
        }

        public SalesFunnelOpportunity GetOpportunityByID(long funnelOpportunityID)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<SalesFunnelOpportunity>(f => f.FunnelOpportunityID, Operator.Eq, funnelOpportunityID));
            return _context.db.GetList<SalesFunnelOpportunity>(pg).FirstOrDefault();
        }

        public List<SalesFunnelOpportunityDashboard> Search(string hirarki, string search)
        {
            _sql = "[sales].[spGetListFunnelOpportunityBySearch]";
            var vParams = new DynamicParameters();
            vParams.Add("@Hirarki", hirarki);
            vParams.Add("@Text", search);
            var output = _context.db.Query<SalesFunnelOpportunityDashboard>(_sql, param: (object)vParams, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).ToList();

            return output;
        }

        public List<SalesFunnelOpportunityDashboard> SearchMarketiing(string search)
        {
            _sql = "[sales].[spGetListFunnelOpportunityBySearchMarketing]";
            var vParams = new DynamicParameters();
            vParams.Add("@Text", search);
            var output = _context.db.Query<SalesFunnelOpportunityDashboard>(_sql, param: (object)vParams, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).ToList();

            return output;
        }
    }
}
