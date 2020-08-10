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
    public class EstructuraAreaLogica
    {
        private EstructuraAreaRepositorio _repositorio;
        private ALICORPContexto _contexto;

        public List<EstructuraArea> Listar(int estructuraId)
        {
            using (_contexto = new ALICORPContexto())
            {
                try
                {
                    _repositorio = new EstructuraAreaRepositorio(_contexto.Connection);
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
