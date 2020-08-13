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
    public class VerificacionRepositorio : BaseRepositorio
    {
        #region Constructores
        public VerificacionRepositorio(SqlConnection connection) : base(connection) { }

        public VerificacionRepositorio(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction) { }

        #endregion

        public List<Verificacion> Listar()
        {
            List<Verificacion> lista = new List<Verificacion>();
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Verificacion_Listar";

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Verificacion
                            {
                                Id = rd.GetInt32(0),
                                Nombre = rd.GetString(1),
                                Activo = rd.GetBoolean(2)
                            });
                        }
                        rd.Close();
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al listar las verificaciones.");
            }
        }

        public Verificacion Buscar(int id)
        {
            Verificacion entidad = null;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Verificacion_Buscar";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            entidad = new Verificacion
                            {
                                Id = rd.GetInt32(0),
                                Nombre = rd.GetString(1),
                                Activo = rd.GetBoolean(2)
                            };
                        }
                        rd.Close();
                    }
                }
                return entidad;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al buscar una verificacion.");
            }
        }

        public bool Guardar(Verificacion entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Verificacion_Guardar";

                    cmd.Parameters.AddWithValue("@nombre", entidad.Nombre);
                    cmd.Parameters.AddWithValue("@activo", entidad.Activo);

                    entidad.Id = int.Parse(cmd.ExecuteScalar().ToString());
                    respuesta = entidad.Id > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al guardar una verificacion.");
            }
        }

        public bool Actualizar(Verificacion entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Verificacion_Actualizar";

                    cmd.Parameters.AddWithValue("@id", entidad.Id);
                    cmd.Parameters.AddWithValue("@nombre", entidad.Nombre);
                    cmd.Parameters.AddWithValue("@activo", entidad.Activo);

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al actualizar una verificacion.");
            }
        }
    }
}
