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
    public class EmpleadoRepositorio : BaseRepositorio
    {
        #region Constructores
        public EmpleadoRepositorio(SqlConnection connection) : base(connection) { }

        public EmpleadoRepositorio(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction) { }

        #endregion

        public List<Empleado> Listar()
        {
            List<Empleado> lista = new List<Empleado>();
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Empleado_Listar";

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Empleado
                            {
                                Id = rd.GetInt32(0),
                                CargoId = !rd.IsDBNull(1) ? rd.GetInt32(1) : (int?)null,
                                AreaId = !rd.IsDBNull(2) ? rd.GetInt32(2) : (int?)null,
                                Nombre = !rd.IsDBNull(3) ? rd.GetString(3) : null,
                                ApellidoPaterno = !rd.IsDBNull(4) ? rd.GetString(4) : null,
                                ApellidoMaterno = !rd.IsDBNull(5) ? rd.GetString(5) : null,
                                NroDocumento = !rd.IsDBNull(6) ? rd.GetString(6) : null,
                                Correo = !rd.IsDBNull(7) ? rd.GetString(7) : null,
                                Telefono = !rd.IsDBNull(8) ? rd.GetString(8) : null,
                                Cargo = rd.IsDBNull(1) ? null : new Cargo { Descripcion = rd.GetString(9) },
                                Area = rd.IsDBNull(2) ? null : new Area { Descripcion = rd.GetString(10) }
                            });
                        }
                        rd.Close();
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al listar los empleados.");
            }
        }

        public Empleado Buscar(int id)
        {
            Empleado entidad = null;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Empleado_Buscar";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            entidad = new Empleado
                            {
                                Id = rd.GetInt32(0),
                                CargoId = !rd.IsDBNull(1) ? rd.GetInt32(1) : (int?)null,
                                AreaId = !rd.IsDBNull(2) ? rd.GetInt32(2) : (int?)null,
                                Nombre = !rd.IsDBNull(3) ? rd.GetString(3) : null,
                                ApellidoPaterno = !rd.IsDBNull(4) ? rd.GetString(4) : null,
                                ApellidoMaterno = !rd.IsDBNull(5) ? rd.GetString(5) : null,
                                NroDocumento = !rd.IsDBNull(6) ? rd.GetString(6) : null,
                                Correo = !rd.IsDBNull(7) ? rd.GetString(7) : null,
                                Telefono = !rd.IsDBNull(8) ? rd.GetString(8) : null
                            };
                        }
                        rd.Close();
                    }
                }
                return entidad;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al buscar un empleado.");
            }
        }

        public bool Guardar(Empleado entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Empleado_Guardar";

                    cmd.Parameters.AddWithValue("@cargoid", entidad.CargoId ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@areaid", entidad.AreaId ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@nombre", entidad.Nombre);
                    cmd.Parameters.AddWithValue("@apellidopaterno", entidad.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@apellidomaterno", entidad.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@nrodocumento", entidad.NroDocumento);
                    cmd.Parameters.AddWithValue("@correo", entidad.Correo ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@telefono", entidad.Telefono ?? Convert.DBNull);

                    entidad.Id = int.Parse(cmd.ExecuteScalar().ToString());
                    respuesta = entidad.Id > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al guardar un empleado.");
            }
        }

        public bool Actualizar(Empleado entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Empleado_Actualizar";

                    cmd.Parameters.AddWithValue("@id", entidad.Id);
                    cmd.Parameters.AddWithValue("@cargoid", entidad.CargoId ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@areaid", entidad.AreaId ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@nombre", entidad.Nombre);
                    cmd.Parameters.AddWithValue("@apellidopaterno", entidad.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@apellidomaterno", entidad.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@nrodocumento", entidad.NroDocumento);
                    cmd.Parameters.AddWithValue("@correo", entidad.Correo ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@telefono", entidad.Telefono ?? Convert.DBNull);

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al actualizar un empleado.");
            }
        }
    }
}
