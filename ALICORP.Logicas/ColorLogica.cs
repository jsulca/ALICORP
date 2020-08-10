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
    public class ColorLogica
    {
        private ColorRepositorio _repositorio;
        private ALICORPContexto _contexto;

        #region Sin Transaccion

        public List<Color> Listar()
        {
            using (_contexto = new ALICORPContexto())
            {
                try
                {
                    _repositorio = new ColorRepositorio(_contexto.Connection);
                    return _repositorio.Listar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Color Buscar(int id)
        {
            using (_contexto = new ALICORPContexto())
            {
                try
                {
                    _repositorio = new ColorRepositorio(_contexto.Connection);
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

        public bool Guardar(Color entidad)
        {
            using (_contexto = new ALICORPContexto(true))
            {
                bool respuesta = false;
                try
                {
                    _repositorio = new ColorRepositorio(_contexto.Connection, _contexto.Transaction);
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

        public bool Actualizar(Color entidad)
        {
            using (_contexto = new ALICORPContexto(true))
            {
                bool respuesta = false;
                try
                {
                    _repositorio = new ColorRepositorio(_contexto.Connection, _contexto.Transaction);
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
