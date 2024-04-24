using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using DapperExtensions;
using DQCustomer.BusinessObject;
using DQCustomer.DataAccess.Interfaces;
using DQCustomer.BusinessObject.Additional;
using DQCustomer.BusinessObject.ViewModel;

namespace DQCustomer.DataAccess.Repositories
{
    public class CustomerCardFileRepository : Repository<FileCustomerCard>, ICustomerCardFileRepository
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private string _sql;
        public CustomerCardFileRepository(IDbTransaction transaction, IDapperContext context) : base(transaction, context)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public List<Req_CustomerCardFileGetByCustomerGenID_ViewModel> GetCustomerCardFileByCustomerGenID(long customerGenID)
        {
            _sql = "[cp].[spGetCustomerCardFileByCustomerGenID]";
            var vParams = new DynamicParameters();
            vParams.Add("@CustomerGenID", customerGenID);
            var output = _context.db.Query<Req_CustomerCardFileGetByCustomerGenID_ViewModel>(_sql, param: vParams, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).ToList();
            return output;
        }

        public bool InsertCustomerCardFile(Req_CustomerCardFileInsert_ViewModel objEntity, string extension, byte[] imageFile)
        {
            _sql = "[cp].[spInsertCustomerCardFileByGenID]";
            var vParams = new DynamicParameters();
            vParams.Add("@CustomerGenID", objEntity.CustomerGenID);
            vParams.Add("@ImageFile", imageFile);
            vParams.Add("@Extension", extension);
            vParams.Add("@LastModifyUserID", objEntity.LastModifyUserID);

            var output = _context.db.Execute(_sql, param: vParams, transaction: _transaction, commandTimeout: null, commandType: CommandType.StoredProcedure);
            return output == 1 ? true : false;
        }
        
        public bool DeleteCustomerCardFile(long CustomerCardID)
        {
            _sql = "[cp].[spInsertRequestNewCustomer]";
            var vParams = new DynamicParameters();
            vParams.Add("@CustomerCardID", CustomerCardID);

            var output = _context.db.Execute(_sql, param: vParams, transaction: _transaction, commandTimeout: null, commandType: CommandType.StoredProcedure);
            return output == 1 ? true : false;
        }

    }
}
