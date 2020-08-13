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
    public class RespuestaRepositorio : BaseRepositorio
    {
        #region Constructores
        public RespuestaRepositorio(SqlConnection connection) : base(connection) { }

        public RespuestaRepositorio(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction) { }

        #endregion

        public List<Respuesta> Listar(int verificacionId)
        {
            List<Respuesta> lista = new List<Respuesta>();
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Respuesta_Listar";

                    cmd.Parameters.AddWithValue("@verificacionid", verificacionId);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Respuesta
                            {
                                PreguntaId = rd.GetInt32(0),
                                Valor = rd.GetInt32(1),
                                Descripcion = !rd.IsDBNull(2) ? rd.GetString(2) : null
                            });
                        }
                        rd.Close();
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al listar las respuestas.");
            }
        }

        public bool Guardar(Respuesta entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Respuesta_Guardar";

                    cmd.Parameters.AddWithValue("@preguntaid", entidad.PreguntaId);
                    cmd.Parameters.AddWithValue("@valor", entidad.Valor);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion ?? Convert.DBNull);

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al guardar una respuesta.");
            }
        }

        public bool Limpiar(int preguntaId)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Respuesta_Limpiar";

                    cmd.Parameters.AddWithValue("@preguntaid", preguntaId);

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al limpiar respuestas.");
            }
        }
    }
}
