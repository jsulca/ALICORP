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
    public class EstructuraInstanciaLogica
    {
        private EstructuraInstanciaRepositorio _repositorio;
        private ALICORPContexto _contexto;

        public List<EstructuraInstancia> Listar(int estructuraId)
        {
            using (_contexto = new ALICORPContexto())
            {
                try
                {
                    _repositorio = new EstructuraInstanciaRepositorio(_contexto.Connection);
                    return _repositorio.Listar(estructuraId);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
