using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using ALICORP.Contextos.Common;

namespace ALICORP.Contextos
{
    public class ALICORPContexto : IDisposable
    {
        public readonly SqlConnection Connection;
        public readonly SqlTransaction Transaction;

        public ALICORPContexto(bool withTransaction = false)
        {
            try
            {
                Connection = new SqlConnection(AppSettings.StrConnection);
                Connection.Open();
                if (withTransaction) Transaction = Connection.BeginTransaction();
            }
            catch (Exception)
            {
                throw new Exception("Error al abrir la conexion.");
            }
        }

        public void Dispose()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
                Connection.Close();
        }
    }
}
