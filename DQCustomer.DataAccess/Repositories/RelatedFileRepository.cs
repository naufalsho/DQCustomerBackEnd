using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using DapperExtensions;
using DQCustomer.BusinessObject;
using DQCustomer.DataAccess.Interfaces;

namespace DQCustomer.DataAccess.Repositories
{
    public class RelatedFileRepository : Repository<CpRelatedFile>, IRelatedFileRepository
    {
        private IDapperContext _context;
        private IDbTransaction _transaction;
        private string _sql;
        public RelatedFileRepository(IDbTransaction transaction, IDapperContext context) : base(transaction, context)
        {
            this._context = context;
            this._transaction = transaction;
        }

        public List<CpRelatedFile> GetRelatedFileByCustomerID(long customerID)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CpRelatedFile>(c => c.CustomerID, Operator.Eq, customerID));
            return _context.db.GetList<CpRelatedFile>(pg).ToList();
        }

        public CpRelatedFile GetRelatedFileById(long Id)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CpRelatedFile>(c => c.RFileID, Operator.Eq, Id));
            return _context.db.GetList<CpRelatedFile>(pg).FirstOrDefault();
        }
        public CpRelatedFile GetRelatedFileByDocumentPath(string documentPath)
        {
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<CpRelatedFile>(c => c.DocumentPath, Operator.Eq, documentPath));
            return _context.db.GetList<CpRelatedFile>(pg).FirstOrDefault();
        }
        public string PathCustomerProfileRelated()
        {
            _sql = "[cp].[spGetPathCustomerProfileRelated]";
            var output = _context.db.Query<string>(_sql, param: null, transaction: _transaction, buffered: false, commandTimeout: null, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return output;
        }
    }
}
