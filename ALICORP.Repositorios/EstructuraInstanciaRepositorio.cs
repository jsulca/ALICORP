using ALICORP.Entidades;
using ALICORP.Repositorios.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Management.Instrumentation;
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
        
        public List<EstructuraInstancia> Listar(int estructuraId)
        {
            List<EstructuraInstancia> lista = new List<EstructuraInstancia>();
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_EstructuraInstancia_Listar";

                    cmd.Parameters.AddWithValue("@estructuraid", estructuraId);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new EstructuraInstancia
                            {
                                EstructuraId = rd.GetInt32(0),
                                InstanciaId = rd.GetInt32(1)
                            });
                        }
                        rd.Close();
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al listar las instancias de una estructura.");
            }
        }

        public void Guardar(EstructuraInstancia entidad)
        {
            try
            {
                Guardar(new List<EstructuraInstancia> { entidad });
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al guardar una instancia en una estructura.");
            }
        }

        public void Guardar(List<EstructuraInstancia> entidades)
        {
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_EstructuraInstancia_Guardar";

                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@estructuraid" });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@instanciaid" });

                    foreach (var entidad in entidades)
                    {
                        cmd.Parameters["@estructuraid"].Value = entidad.EstructuraId;
                        cmd.Parameters["@instanciaid"].Value = entidad.InstanciaId;

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
                    cmd.CommandText = "usp_EstructuraInstancia_Limpiar";

                    cmd.Parameters.AddWithValue("@estructuraid", estructuraId);

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al limpiar las instancias por estructura.");
            }
        }
    }
}
