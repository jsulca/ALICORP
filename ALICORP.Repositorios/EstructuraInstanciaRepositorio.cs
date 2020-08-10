using ALICORP.Entidades;
using ALICORP.Repositorios.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Repositorios
{
    public class EstructuraInstanciaRepositorio : BaseRepositorio
    {
        #region Constructores

        public EstructuraInstanciaRepositorio(SqlConnection connection) : base(connection) { }

        public EstructuraInstanciaRepositorio(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction) { }

        #endregion
    }
}
