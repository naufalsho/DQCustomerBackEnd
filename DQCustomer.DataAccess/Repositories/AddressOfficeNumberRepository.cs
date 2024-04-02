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
    public class AddressOfficeNumberRepository : Repository<CpAddressOfficeNumber>, IAddressOfficeNumberRepository
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private string _sql;
        public AddressOfficeNumberRepository(IDbTransaction transaction, IDapperContext context) : base(transaction, context)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public bool InsertAddressOfficeNumber(CpAddressOfficeNumber objEntity) {
            return true;
        }

        public CpAddressOfficeNumber GetAddressOfficeNumberById(long Id)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CpAddressOfficeNumber>(c => c.AddressOfficeNumberID, Operator.Eq, Id));
            return _context.db.GetList<CpAddressOfficeNumber>(pg).FirstOrDefault();
        }

        public List<CpAddressOfficeNumber> GetAddressOfficeNumberByCustomerGenId(long customerGenId)
        {
            _sql = "[cp].[spGetAddressOfficeNumberById]";
            var vParams = new DynamicParameters();
            vParams.Add("@CustomerGenID", customerGenId);
            vParams.Add("@CustomerID", null);
            var output = _context.db.Query<CpAddressOfficeNumber>(_sql, param: vParams, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).ToList();
            return output;
        }
        
        public List<CpAddressOfficeNumber> GetAddressOfficeNumberByCustomerId(long customerId)
        {
            _sql = "[cp].[spGetAddressOfficeNumberById]";
            var vParams = new DynamicParameters();
            vParams.Add("@CustomerGenID", null);
            vParams.Add("@CustomerID", customerId);
            var output = _context.db.Query<CpAddressOfficeNumber>(_sql, param: vParams, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).ToList();
            return output;
        }
    }
}