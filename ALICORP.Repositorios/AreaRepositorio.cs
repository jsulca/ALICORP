﻿using ALICORP.Entidades;
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
    public class AreaRepositorio : BaseRepositorio
    {
        #region Constructores

        public AreaRepositorio(SqlConnection connection) : base(connection) { }

        public AreaRepositorio(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction) { }

        #endregion

        public List<Area> Listar()
        {
            List<Area> lista = new List<Area>();
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Area_Listar";

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Area
                            {
                                Id = rd.GetInt32(0),
                                Codigo = !rd.IsDBNull(1) ? rd.GetString(1) : null,
                                Descripcion = rd.GetString(2),
                                ColorFondo = !rd.IsDBNull(3) ? rd.GetString(3) : null,
                                ColorTexto = !rd.IsDBNull(4) ? rd.GetString(4) : null
                            });
                        }
                        rd.Close();
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al listar las áreas.");
            }
        }

        public Area Buscar(int id)
        {
            Area entidad = null;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Area_Buscar";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            entidad = new Area
                            {
                                Id = id,
                                Codigo = !rd.IsDBNull(0) ? rd.GetString(0) : null,
                                Descripcion = rd.GetString(1),
                                ColorFondo = !rd.IsDBNull(2) ? rd.GetString(2) : null,
                                ColorTexto = !rd.IsDBNull(3) ? rd.GetString(3) : null
                            };
                        }
                        rd.Close();
                    }
                }
                return entidad;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al buscar una área.");
            }
        }

        public bool Guardar(Area entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Area_Guardar";

                    cmd.Parameters.AddWithValue("@codigo", entidad.Codigo ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion);
                    cmd.Parameters.AddWithValue("@colorfondo", entidad.ColorFondo ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@colortexto", entidad.ColorTexto ?? Convert.DBNull);

                    entidad.Id = int.Parse(cmd.ExecuteScalar().ToString());
                    respuesta = entidad.Id > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al guardar una área.");
            }
        }

        public bool Actualizar(Area entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Area_Actualizar";

                    cmd.Parameters.AddWithValue("@id", entidad.Id);
                    cmd.Parameters.AddWithValue("@codigo", entidad.Codigo ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion);
                    cmd.Parameters.AddWithValue("@colorfondo", entidad.ColorFondo ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@colortexto", entidad.ColorTexto ?? Convert.DBNull);

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al actualizar una área.");
            }
        }
    }
}
