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
    public class CategoriaRepositorio : BaseRepositorio
    {
        #region Constructores
        public CategoriaRepositorio(SqlConnection connection) : base(connection) { }

        public CategoriaRepositorio(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction) { }

        #endregion

        public List<Categoria> Listar(int verificacionId)
        {
            List<Categoria> lista = new List<Categoria>();
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Categoria_Listar";

                    cmd.Parameters.AddWithValue("@verificacionid", verificacionId);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Categoria
                            {
                                Id = rd.GetInt32(0),
                                VerificacionId = rd.GetInt32(1),
                                Descripcion = rd.GetString(2),
                                Orden = rd.GetInt32(3),
                                Eliminado = rd.GetBoolean(4)
                            });
                        }
                        rd.Close();
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al listar las categorias.");
            }
        }

        public bool Guardar(Categoria entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Categoria_Guardar";

                    cmd.Parameters.AddWithValue("@verificacionid", entidad.VerificacionId);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@orden", entidad.Orden);
                    cmd.Parameters.AddWithValue("@eliminado", entidad.Eliminado);

                    entidad.Id = int.Parse(cmd.ExecuteScalar().ToString());
                    respuesta = entidad.Id > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al guardar una categoria.");
            }
        }

        public bool Actualizar(Categoria entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Categoria_Actualizar";

                    cmd.Parameters.AddWithValue("@id", entidad.Id);
                    cmd.Parameters.AddWithValue("@verificacionid", entidad.VerificacionId);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@orden", entidad.Orden);
                    cmd.Parameters.AddWithValue("@eliminado", entidad.Eliminado);

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al actualizar una categoria.");
            }
        }
    }
}
