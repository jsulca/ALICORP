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
    public class CompromisoLogica
    {
        private CompromisoRepositorio _repositorio;
        private ALICORPContexto _contexto;

        #region Sin Transaccion

        public List<Compromiso> ListarPorPagina(CompromisoFiltro filtro, int page, int pageSize, ref int totalRows)
        {
            using (_contexto = new ALICORPContexto())
            {
                try
                {
                    _repositorio = new CompromisoRepositorio(_contexto.Connection);
                    return _repositorio.ListarPorPagina(filtro, page, pageSize, ref totalRows, "id", "DESC");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Compromiso Buscar(int id)
        {
            using (_contexto = new ALICORPContexto())
            {
                try
                {
                    _repositorio = new CompromisoRepositorio(_contexto.Connection);
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

        public void Guardar(Compromiso entidad)
        {
            using (_contexto = new ALICORPContexto(true))
            {
                try
                {
                    _repositorio = new CompromisoRepositorio(_contexto.Connection, _contexto.Transaction);

                    entidad.Codigo = (_repositorio.Contar(entidad.EstructuraId) + 1).ToString("D10");

                    if (_repositorio.Guardar(entidad))
                    {

                    }
                    _contexto.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    _contexto?.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Actualizar(Compromiso entidad)
        {
            using (_contexto = new ALICORPContexto(true))
            {
                try
                {
                    _repositorio = new CompromisoRepositorio(_contexto.Connection, _contexto.Transaction);
                    _repositorio.Actualizar(entidad);
                    _contexto.Transaction.Commit();
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
