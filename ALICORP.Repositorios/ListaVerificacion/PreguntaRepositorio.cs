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
    public class PreguntaRepositorio : BaseRepositorio
    {
        #region Constructores
        public PreguntaRepositorio(SqlConnection connection) : base(connection) { }

        public PreguntaRepositorio(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction) { }

        #endregion

        public List<Pregunta> Listar(int verificacionId)
        {
            List<Pregunta> lista = new List<Pregunta>();
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Pregunta_Listar";

                    cmd.Parameters.AddWithValue("@verificacionid", verificacionId);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Pregunta
                            {
                                Id = rd.GetInt32(0),
                                CategoriaId = rd.GetInt32(1),
                                Orden = rd.GetInt32(2),
                                Titulo = rd.GetString(3),
                                Descripcion = !rd.IsDBNull(4) ? rd.GetString(4) : null,                                
                                Eliminado = rd.GetBoolean(5)
                            });
                        }
                        rd.Close();
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al listar las preguntas.");
            }
        }

        public bool Guardar(Pregunta entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Pregunta_Guardar";

                    cmd.Parameters.AddWithValue("@categoriaid", entidad.CategoriaId);
                    cmd.Parameters.AddWithValue("@orden", entidad.Orden);
                    cmd.Parameters.AddWithValue("@titulo", entidad.Titulo);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion ?? Convert.DBNull);                    
                    cmd.Parameters.AddWithValue("@eliminado", entidad.Eliminado);

                    entidad.Id = int.Parse(cmd.ExecuteScalar().ToString());
                    respuesta = entidad.Id > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al guardar una pregunta.");
            }
        }

        public bool Actualizar(Pregunta entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Pregunta_Actualizar";

                    cmd.Parameters.AddWithValue("@id", entidad.Id);
                    cmd.Parameters.AddWithValue("@categoriaid", entidad.CategoriaId);
                    cmd.Parameters.AddWithValue("@orden", entidad.Orden);
                    cmd.Parameters.AddWithValue("@titulo", entidad.Titulo);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@eliminado", entidad.Eliminado);

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al actualizar una pregunta.");
            }
        }
    }
}
