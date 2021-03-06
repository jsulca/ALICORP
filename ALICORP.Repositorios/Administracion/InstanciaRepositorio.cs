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
    public class InstanciaRepositorio : BaseRepositorio
    {
        #region Constructores

        public InstanciaRepositorio(SqlConnection connection) : base(connection) { }

        public InstanciaRepositorio(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction) { }

        #endregion

        public List<Instancia> Listar()
        {
            List<Instancia> lista = new List<Instancia>();
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Instancia_Listar";

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Instancia
                            {
                                Id = rd.GetInt32(0),
                                Abreviatura = !rd.IsDBNull(1) ? rd.GetString(1) : null,
                                Descripcion = rd.GetString(2),
                                ColorFondoId = rd.GetInt32(3),
                                ColorTextoId = rd.GetInt32(4)
                            });
                        }
                        rd.Close();
                    }
                }
                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al listar las instancias.");
            }
        }

        public Instancia Buscar(int id)
        {
            Instancia entidad = null;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Instancia_Buscar";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            entidad = new Instancia
                            {
                                Id = rd.GetInt32(0),
                                Abreviatura = !rd.IsDBNull(1) ? rd.GetString(1) : null,
                                Descripcion = rd.GetString(2),
                                ColorFondoId = rd.GetInt32(3),
                                ColorTextoId = rd.GetInt32(4)
                            };
                        }
                        rd.Close();
                    }
                }
                return entidad;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al buscar una instancia.");
            }
        }

        public bool Guardar(Instancia entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Instancia_Guardar";

                    cmd.Parameters.AddWithValue("@abreviatura", entidad.Abreviatura?.ToUpper() ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion.ToUpper());
                    cmd.Parameters.AddWithValue("@colorfondoid", entidad.ColorFondoId);
                    cmd.Parameters.AddWithValue("@colortextoid", entidad.ColorTextoId);

                    entidad.Id = int.Parse(cmd.ExecuteScalar().ToString());
                    respuesta = entidad.Id > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al guardar una instancia.");
            }
        }

        public bool Actualizar(Instancia entidad)
        {
            bool respuesta = false;
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Instancia_Actualizar";

                    cmd.Parameters.AddWithValue("@id", entidad.Id);
                    cmd.Parameters.AddWithValue("@abreviatura", entidad.Abreviatura?.ToUpper() ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion.ToUpper());
                    cmd.Parameters.AddWithValue("@colorfondoid", entidad.ColorFondoId);
                    cmd.Parameters.AddWithValue("@colortextoid", entidad.ColorTextoId);

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw new Exception("Ocurrió un problema al actualizar una instancia.");
            }
        }
    }
}
