using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataManagement.Repository
{
    public class BaseRepository : IDisposable
    {
        protected readonly IDbConnection con;
        protected readonly IDbConnection conIMDWH;

        protected BaseRepository()
        {
            string connectionstring = @"Data source=.\CUST;Initial Catalog=IMSTG;Integrated Security=SSPI;";
            con = new SqlConnection(connectionstring);
            connectionstring = @"Data source=.\CUST;Initial Catalog=IMDWH;Integrated Security=SSPI;";
            conIMDWH = new SqlConnection(connectionstring);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BaseRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
