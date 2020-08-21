using ALICORP.Contextos;
using ALICORP.Entidades;
using ALICORP.Entidades.Filtros;
using ALICORP.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Logicas
{
    public class EstructuraLogica
    {
        private EstructuraRepositorio _repositorio;
        private ALICORPContexto _contexto;

        #region Sin Transaccion

        public List<Estructura> Listar(EstructuraFiltro filtro = null)
        {
            using (_contexto = new ALICORPContexto())
            {
                try
                {
                    _repositorio = new EstructuraRepositorio(_contexto.Connection);
                    return _repositorio.Listar(filtro);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Estructura Buscar(int id)
        {
            using (_contexto = new ALICORPContexto())
            {
                try
                {
                    _repositorio = new EstructuraRepositorio(_contexto.Connection);
                    return _repositorio.Buscar(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool TieneTablero(int id)
        {
            using (_contexto = new ALICORPContexto())
            {
                try
                {
                    _repositorio = new EstructuraRepositorio(_contexto.Connection);
                    return _repositorio.TieneTablero(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string Ruta(int id)
        {
            using (_contexto = new ALICORPContexto())
            {
                try
                {
                    _repositorio = new EstructuraRepositorio(_contexto.Connection);
                    return _repositorio.Ruta(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region Con Transaccion

        public bool Guardar(Estructura entidad)
        {
            using (_contexto = new ALICORPContexto(true))
            {
                bool respuesta = false;
                try
                {
                    _repositorio = new EstructuraRepositorio(_contexto.Connection, _contexto.Transaction);
                    respuesta = _repositorio.Guardar(entidad);

                    _contexto.Transaction.Commit();

                    return respuesta;
                }
                catch (Exception ex)
                {
                    _contexto?.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Guardar(int estructuraId, List<EstructuraInstancia> instancias, List<EstructuraArea> areas)
        {
            EstructuraInstanciaRepositorio estructuraInstanciaRepositorio;
            EstructuraAreaRepositorio estructuraAreaRepositorio;

            using (_contexto = new ALICORPContexto(true))
            {
                try
                {
                    estructuraInstanciaRepositorio = new EstructuraInstanciaRepositorio(_contexto.Connection, _contexto.Transaction);
                    estructuraAreaRepositorio = new EstructuraAreaRepositorio(_contexto.Connection, _contexto.Transaction);

                    estructuraInstanciaRepositorio.Limpiar(estructuraId);
                    if (instancias != null) estructuraInstanciaRepositorio.Guardar(instancias);

                    estructuraAreaRepositorio.Limpiar(estructuraId);
                    if (areas != null) estructuraAreaRepositorio.Guardar(areas);

                    _contexto.Transaction.Commit();

                }
                catch (Exception ex)
                {
                    _contexto?.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        public bool Actualizar(Estructura entidad)
        {
            using (_contexto = new ALICORPContexto(true))
            {
                bool respuesta = false;
                try
                {
                    _repositorio = new EstructuraRepositorio(_contexto.Connection, _contexto.Transaction);
                    respuesta = _repositorio.Actualizar(entidad);

                    _contexto.Transaction.Commit();

                    return respuesta;
                }
                catch (Exception ex)
                {
                    _contexto?.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        #endregion
    }
}
