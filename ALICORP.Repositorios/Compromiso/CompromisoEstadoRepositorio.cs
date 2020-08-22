using ALICORP.Entidades;
using ALICORP.Entidades.Filtros;
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
    public class CompromisoEstadoRepositorio : BaseRepositorio
    {
        #region Constructores

        public CompromisoEstadoRepositorio(SqlConnection connection) : base(connection) { }

        public CompromisoEstadoRepositorio(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction) { }

        #endregion

        public List<CompromisoEstado> Listar(int compromisoId)
        {
            List<CompromisoEstado> lista = new List<CompromisoEstado>();

            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_CompromisoEstado_Listar";

                    cmd.Parameters.AddWithValue("@compromisoid", compromisoId);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new CompromisoEstado
                            {
                                CompromisoId = rd.GetInt32(0),
                                Estado = (EstadoCompromiso)rd.GetInt32(1),
                                FechaRegistro = rd.GetDateTime(2)
                            });
                        }

                        rd.Close();
                    }
                }

                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un error al momento de listar los estados de un compromiso.");
            }
        }

        public bool Guardar(CompromisoEstado entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_CompromisoEstado_Guardar";

                    cmd.Parameters.AddWithValue("@compromisoid", entidad.CompromisoId);
                    cmd.Parameters.AddWithValue("@estado", entidad.Estado);

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }

                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un error al momento de guardar el estado del compromiso.");
            }
        }
    }
}
