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
    public class ColorRepositorio : BaseRepositorio
    {
        #region Constructores

        public ColorRepositorio(SqlConnection connection) : base(connection) { }

        public ColorRepositorio(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction) { }

        #endregion

        public List<Color> Listar()
        {
            List<Color> lista = new List<Color>();
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Color_Listar";

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Color
                            {
                                Id = rd.GetInt32(0),
                                Descripcion = rd.GetString(1),
                                Rgba = !rd.IsDBNull(2) ? rd.GetString(2) : null,
                                Hex = !rd.IsDBNull(3) ? rd.GetString(3) : null,
                                Clase = !rd.IsDBNull(4) ? rd.GetString(4) : null,
                            });
                        }
                        rd.Close();
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al listar los colores.");
            }
        }

        public Color Buscar(int id)
        {
            Color entidad = null;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Color_Buscar";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            entidad = new Color
                            {
                                Id = id,
                                Descripcion = rd.GetString(0),
                                Rgba = !rd.IsDBNull(1) ? rd.GetString(1) : null,
                                Hex = !rd.IsDBNull(2) ? rd.GetString(2) : null,
                                Clase = !rd.IsDBNull(3) ? rd.GetString(3) : null,
                            };
                        }
                        rd.Close();
                    }
                }
                return entidad;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al buscar un color.");
            }
        }

        public bool Guardar(Color entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Color_Guardar";

                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion.ToUpper());
                    cmd.Parameters.AddWithValue("@rgba", entidad.Rgba ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@hex", entidad.Hex ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@clase", entidad.Clase ?? Convert.DBNull);

                    entidad.Id = int.Parse(cmd.ExecuteScalar().ToString());
                    respuesta = entidad.Id > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al guardar un color.");
            }
        }

        public bool Actualizar(Color entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Color_Actualizar";

                    cmd.Parameters.AddWithValue("@id", entidad.Id);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion.ToUpper());
                    cmd.Parameters.AddWithValue("@rgba", entidad.Rgba ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@hex", entidad.Hex ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@clase", entidad.Clase ?? Convert.DBNull);

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al actualizar un color.");
            }
        }
    }
}
