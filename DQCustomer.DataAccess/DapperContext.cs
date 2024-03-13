using DQCustomer.DataAccess.Interfaces;
using Dapper;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DQCustomer.DataAccess
{
    public class DapperContext : IDapperContext
    {

        private readonly string _providerName;
        private readonly string _connectionString;
        private IDbConnection _db;

        public DapperContext(string connection)
        {
            _providerName = "System.Data.SqlClient";
            _connectionString = connection;
        }
        private IDbConnection GetOpenConnection(string providerName, string connectionString)
        {
            DbConnection conn = null;

            try
            {
                if (providerName == "System.Data.SqlClient")
                {
                    conn = new SqlConnection(connectionString);
                }
                else
                {
                    DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);
                    conn = provider.CreateConnection();
                }

                if (conn != null)
                {
                    conn.ConnectionString = connectionString;
                    conn.Open();
                }

            }
            catch(Exception)
            {
                // ignored
            }

            return conn;
        }

        public IDbConnection db => _db ?? (_db = GetOpenConnection(_providerName, _connectionString));


        public int GetLastId()
        {
            var lastId = 0;

            try
            {
                var sql = @"SELECT LAST_INSERT_ROWID()";
                lastId = _db.ExecuteScalar<int>(sql);
            }
            catch
            {
                // ignored
            }

            return lastId;
        }

        public void Dispose()
        {
            if (_db != null)
            {
                try
                {
                    if (_db.State != ConnectionState.Closed)
                        _db.Close();
                }
                finally
                {
                    _db.Dispose();
                }
            }

            GC.SuppressFinalize(this);
        }

    }
}
