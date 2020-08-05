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
    public class EstructuraRepositorio : BaseRepositorio
    {
        #region Constructores

        public EstructuraRepositorio(SqlConnection connection) : base(connection) { }

        public EstructuraRepositorio(SqlConnection connection, SqlTransaction transaction) : base(connection, transaction) { }

        #endregion

        public List<Estructura> Listar()
        {
            List<Estructura> lista = new List<Estructura>();
            try
            {
                using (var cmd = CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Estructura_Listar";

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Estructura
                            {
                                Id = rd.GetInt32(0),
                                PadreId = !rd.IsDBNull(1) ? rd.GetInt32(1) : (int?)null,
                                InstanciaId = !rd.IsDBNull(2) ? rd.GetInt32(2) : (int?)null,
                                Codigo = !rd.IsDBNull(3) ? rd.GetString(3) : null,
                                Descripcion = rd.GetString(4),
                                Tablero = rd.GetBoolean(5),
                                Instancia = rd.IsDBNull(2) ? null : new Instancia { Descripcion = rd.GetString(6) }
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
                                InstanciaId = !rd.IsDBNull(2) ? rd.GetInt32(2) : (int?)null,
                                Codigo = !rd.IsDBNull(3) ? rd.GetString(3) : null,
                                Descripcion = rd.GetString(4),
                                Tablero = rd.GetBoolean(5)
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
                    cmd.Parameters.AddWithValue("@instanciaid", entidad.InstanciaId ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@codigo", entidad.Codigo ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion);
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
                    cmd.Parameters.AddWithValue("@instanciaid", entidad.InstanciaId ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@codigo", entidad.Codigo ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion);

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
    }
}
