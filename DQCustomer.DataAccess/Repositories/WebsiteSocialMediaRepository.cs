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
    public class WebsiteSocialMediaRepository : Repository<CpWebsiteSocialMedia>, IWebsiteSocialMediaRepository
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private string _sql;
        public WebsiteSocialMediaRepository(IDbTransaction transaction, IDapperContext context) : base(transaction, context)
         {
            this._context = context;
            this._transaction = transaction;
        }

        public CpWebsiteSocialMedia GetWebsiteSocialMediaByID(long Id)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CpWebsiteSocialMedia>(c => c.WebsiteSocialMediaID, Operator.Eq, Id));
            return _context.db.GetList<CpWebsiteSocialMedia>(pg).FirstOrDefault();
        }
        public List <Req_CustomerMasterGetWebsiteSocialMediaByGenID_ViewModel> GetWebsiteSocialMediaByGenID(long customerGenID)
        {
            _sql = "[cp].[spGetWebsiteSocialMediaByGenID]";
            var vParams = new DynamicParameters();
            vParams.Add("@CustomerGenID", customerGenID);
            var output = _context.db.Query<Req_CustomerMasterGetWebsiteSocialMediaByGenID_ViewModel>(_sql, param: vParams, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).ToList();
            return output;
        }
    }
}