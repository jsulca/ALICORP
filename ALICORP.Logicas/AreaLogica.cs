using ALICORP.Contextos;
using ALICORP.Entidades;
using ALICORP.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Logicas
{
    public class AreaLogica
    {
        private AreaRepositorio _repositorio;
        private ALICORPContexto _contexto;

        #region Sin Transaccion

        public List<Area> Listar()
        {
            using (_contexto = new ALICORPContexto())
            {
                try
                {
                    _repositorio = new AreaRepositorio(_contexto.Connection);
                    return _repositorio.Listar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Area Buscar(int id)
        {
            using (_contexto = new ALICORPContexto())
            {
                try
                {
                    _repositorio = new AreaRepositorio(_contexto.Connection);
                    return _repositorio.Buscar(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region Con Transaccion

        public bool Guardar(Area entidad)
        {
            using (_contexto = new ALICORPContexto(true))
            {
                bool respuesta = false;
                try
                {
                    _repositorio = new AreaRepositorio(_contexto.Connection, _contexto.Transaction);
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

        public bool Actualizar(Area entidad)
        {
            using (_contexto = new ALICORPContexto(true))
            {
                bool respuesta = false;
                try
                {
                    _repositorio = new AreaRepositorio(_contexto.Connection, _contexto.Transaction);
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
