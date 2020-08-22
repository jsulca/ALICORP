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
    public class CompromisoRepositorio : BaseRepositorio
    {
        #region Constructores

        public CompromisoRepositorio(SqlConnection connection) : base(connection) { }

        public CompromisoRepositorio(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction) { }

        #endregion

        public List<Compromiso> ListarPorPagina(CompromisoFiltro filtro, int page, int pageSize, ref int totalRows, string orderBy, string sortBy)
        {
            List<Compromiso> lista = new List<Compromiso>();
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Compromiso_ListarPorPagina";

                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pagesize", pageSize);
                    cmd.Parameters.AddWithValue("@orderby", orderBy);
                    cmd.Parameters.AddWithValue("@sortby", sortBy);

                    cmd.Parameters.AddWithValue("@tableroid", filtro?.TableroId ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@codigo", filtro?.Codigo ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@descripcion", filtro?.Descripcion ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@fecharegistrodesde", filtro?.FechaRegistroDesde ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@fecharegistrohasta", filtro?.FechaRegistroHasta ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@estado", filtro?.Estado ?? Convert.DBNull);

                    cmd.Parameters.Add("@totalrows", SqlDbType.Int).Direction = ParameterDirection.Output;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Compromiso
                            {
                                Id = rd.GetInt32(0),
                                Codigo = rd.GetString(1),
                                Descripcion = rd.GetString(2),
                                FechaRegistro = rd.GetDateTime(3),
                                Estado = (EstadoCompromiso)rd.GetInt32(4),
                                Tablero = new Estructura { Descripcion = rd.GetString(5) }
                            });
                        }
                        rd.Close();
                        totalRows = int.Parse(cmd.Parameters["@totalrows"].Value.ToString());
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un error al momento de listar los compromisos por pagina.");
            }
        }

        public Compromiso Buscar(int id)
        {
            Compromiso entidad = null;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Compromiso_Buscar";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            entidad = new Compromiso
                            {
                                Codigo = rd.GetString(0),
                                Descripcion = rd.GetString(1),
                                Impacto = !rd.IsDBNull(2) ? rd.GetString(2) : null,
                                Estado = (EstadoCompromiso)rd.GetInt32(3),
                                EstructuraId = rd.GetInt32(4),
                                Detalle = !rd.IsDBNull(5) ? rd.GetString(5) : null,
                                TableroId = rd.GetInt32(6),
                                Respuesta = !rd.IsDBNull(7) ? rd.GetString(7) : null,
                                FechaRegistro = rd.GetDateTime(8)
                            };
                        }
                        rd.Close();
                    }
                }

                return entidad;
            }
            catch (Exception)
            {
                throw new Exception("Error al momento de buscar el compromiso.");
            }
        }

        public bool Guardar(Compromiso entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Compromiso_Guardar";

                    cmd.Parameters.AddWithValue("@codigo", entidad.Codigo);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion);
                    cmd.Parameters.AddWithValue("@detalle", entidad.Detalle ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@impacto", entidad.Impacto ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@estado", entidad.Estado);
                    cmd.Parameters.AddWithValue("@estructuraid", entidad.EstructuraId);
                    cmd.Parameters.AddWithValue("@tableroid", entidad.TableroId);

                    entidad.Id = int.Parse(cmd.ExecuteScalar().ToString());

                    respuesta = entidad.Id > 0;
                }

                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un error al momento de guardar el compromiso.");
            }
        }

        public bool Actualizar(Compromiso entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Compromiso_Actualizar";

                    cmd.Parameters.AddWithValue("@id", entidad.Id);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion);
                    cmd.Parameters.AddWithValue("@detalle", entidad.Detalle ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@impacto", entidad.Impacto ?? Convert.DBNull);

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }

                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un error al momento de actualizar el compromiso.");
            }
        }

        public int Contar(int estructuraId)
        {
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Compromiso_Contar";

                    cmd.Parameters.AddWithValue("@estructuraid", estructuraId);

                    return int.Parse(cmd.ExecuteScalar().ToString());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un error al momento de contar los compromisos.");
            }
        }

        public bool Finalizar(int id, string respuesta, EstadoCompromiso estado)
        {
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Compromiso_Finalizar";

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@respuesta", respuesta);

                    return  cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocurrio un error al momento de finalizar el compromiso.");
            }
        }
    }
}
