using System;
using System.Data;

namespace DQCustomer.DataAccess.Interfaces
{
    public interface IDapperContext: IDisposable
    {
        IDbConnection db { get; }
        int GetLastId();
    }
}
