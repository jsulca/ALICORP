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
    public class EstructuraRepositorio : BaseRepositorio
    {
        #region Constructores

        public EstructuraRepositorio(SqlConnection connection) : base(connection) { }

        public EstructuraRepositorio(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction) { }

        #endregion

        public List<Estructura> Listar(EstructuraFiltro filtro)
        {
            List<Estructura> lista = new List<Estructura>();
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Estructura_Listar";

                    cmd.Parameters.AddWithValue("@tablero", filtro?.Tablero ?? Convert.DBNull);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Estructura
                            {
                                Id = rd.GetInt32(0),
                                PadreId = !rd.IsDBNull(1) ? rd.GetInt32(1) : (int?)null,
                                Codigo = !rd.IsDBNull(2) ? rd.GetString(2) : null,
                                Descripcion = rd.GetString(3),
                                Tablero = rd.GetBoolean(4)
                            });
                        }
                        rd.Close();
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al listar las estructuras.");
            }
        }

        public Estructura Buscar(int id)
        {
            Estructura entidad = null;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Estructura_Buscar";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            entidad = new Estructura
                            {
                                Id = rd.GetInt32(0),
                                PadreId = !rd.IsDBNull(1) ? rd.GetInt32(1) : (int?)null,
                                Codigo = !rd.IsDBNull(2) ? rd.GetString(2) : null,
                                Descripcion = rd.GetString(3),
                                Tablero = rd.GetBoolean(4)
                            };
                        }
                        rd.Close();
                    }
                }
                return entidad;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al buscar una estructura.");
            }
        }

        public bool Guardar(Estructura entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Estructura_Guardar";

                    cmd.Parameters.AddWithValue("@padreid", entidad.PadreId ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@codigo", entidad.Codigo?.ToUpper() ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion.ToUpper());
                    cmd.Parameters.AddWithValue("@tablero", entidad.Tablero);

                    entidad.Id = int.Parse(cmd.ExecuteScalar().ToString());
                    respuesta = entidad.Id > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al guardar una estructura.");
            }
        }

        public bool Actualizar(Estructura entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Estructura_Actualizar";

                    cmd.Parameters.AddWithValue("@id", entidad.Id);
                    cmd.Parameters.AddWithValue("@codigo", entidad.Codigo?.ToUpper() ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion.ToUpper());
                    cmd.Parameters.AddWithValue("@tablero", entidad.Tablero);

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al actualizar una estructura.");
            }
        }

        public bool TieneTablero(int id)
        {
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Estructura_TieneTablero";

                    cmd.Parameters.AddWithValue("@id", id);

                    return bool.Parse(cmd.ExecuteScalar().ToString());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al validar.");
            }
        }

        public string Ruta(int id)
        {
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Estructura_Ruta";

                    cmd.Parameters.AddWithValue("@id", id);

                    return cmd.ExecuteScalar().ToString();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al crear la ruta.");
            }
        }
    }
}
