using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using DQCustomer.BusinessObject;
using DQCustomer.BusinessObject.ViewModel;
using DQCustomer.DataAccess.Interfaces;

namespace DQCustomer.DataAccess.Repositories
{
    public class CustomerSuccessStoryRepository : Repository<CpCustomerSuccessStory>, ICustomerSuccessStoryRepository
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private string _sql;
        public CustomerSuccessStoryRepository(IDbTransaction transaction, IDapperContext context) : base(transaction, context)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public List<string> GetCustomerStoriesByCustomerID(long funnelID)
        {
            _sql = "[cp].[spGetModifySuccessStories]";
            var vParams = new DynamicParameters();
            vParams.Add("@FunnelID", funnelID);
            var output = _context.db.Query<string>(_sql, param: (object)vParams, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).ToList();
            return output;
        }
    }
}
