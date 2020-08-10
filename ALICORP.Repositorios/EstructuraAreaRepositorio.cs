using ALICORP.Entidades;
using ALICORP.Repositorios.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Repositorios
{
    public class EstructuraAreaRepositorio : BaseRepositorio
    {
        #region Constructores

        public EstructuraAreaRepositorio(SqlConnection connection) : base(connection) { }

        public EstructuraAreaRepositorio(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction) { }

        #endregion

        public List<EstructuraArea> Listar(int estructuraId)
        {
            List<EstructuraArea> lista = new List<EstructuraArea>();
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_EstructuraArea_Listar";

                    cmd.Parameters.AddWithValue("@estructuraid", estructuraId);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new EstructuraArea
                            {
                                EstructuraId = rd.GetInt32(0),
                                AreaId = rd.GetInt32(1)
                            });
                        }
                        rd.Close();
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al listar las áreas de una estructura.");
            }
        }

        public void Guardar(EstructuraArea entidad)
        {
            try
            {
                Guardar(new List<EstructuraArea> { entidad });
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al guardar una estructura con una área.");
            }
        }

        public void Guardar(List<EstructuraArea> entidades)
        {
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_EstructuraArea_Guardar";

                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@estructuraid" } );
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@areaid" } );

                    foreach(var entidad in entidades)
                    {
                        cmd.Parameters["@estructuraid"].Value = entidad.EstructuraId;
                        cmd.Parameters["@areaid"].Value = entidad.AreaId;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al guardar las áreas en una estructura.");
            }
        }

        public bool Limpiar(int estructuraId)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_EstructuraArea_Limpiar";

                    cmd.Parameters.AddWithValue("@estructuraid", estructuraId);

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al limpiar las áreas por estructura.");
            }
        }
    }
}
