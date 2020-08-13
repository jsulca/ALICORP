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
    public class VerificacionLogica
    {
        private VerificacionRepositorio _repositorio;
        private ALICORPContexto _contexto;

        #region Sin Transaccion

        public List<Verificacion> Listar()
        {
            using (_contexto = new ALICORPContexto())
            {
                try
                {
                    _repositorio = new VerificacionRepositorio(_contexto.Connection);
                    return _repositorio.Listar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Verificacion Buscar(int id, bool conDetalles = false)
        {
            CategoriaRepositorio categoriaRepositorio;
            PreguntaRepositorio preguntaRepositorio;
            RespuestaRepositorio respuestaRepositorio;
            using (_contexto = new ALICORPContexto())
            {
                try
                {
                    _repositorio = new VerificacionRepositorio(_contexto.Connection);

                    Verificacion entidad = _repositorio.Buscar(id);

                    if (conDetalles && entidad != null)
                    {
                        categoriaRepositorio = new CategoriaRepositorio(_contexto.Connection);
                        preguntaRepositorio = new PreguntaRepositorio(_contexto.Connection);
                        respuestaRepositorio = new RespuestaRepositorio(_contexto.Connection);

                        List<Categoria> categorias = categoriaRepositorio.Listar(id);

                        List<Pregunta> preguntas = preguntaRepositorio.Listar(id);

                        List<Respuesta> respuestas = respuestaRepositorio.Listar(id);

                        if (preguntas != null && respuestas != null)
                        {
                            foreach (var item in preguntas)
                                item.Respuestas = respuestas.Where(x => x.PreguntaId == item.Id)?.ToList() ?? new List<Respuesta>();
                        }
                        if (categorias != null && preguntas != null)
                        {
                            foreach (var item in categorias)
                                item.Preguntas = preguntas.Where(x => x.CategoriaId == item.Id)?.ToList() ?? new List<Pregunta>();
                        }

                        entidad.Categorias = categorias;
                    }
                    return entidad;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region Con Transaccion

        public void Guardar(Verificacion entidad)
        {
            CategoriaRepositorio categoriaRepositorio;
            PreguntaRepositorio preguntaRepositorio;
            RespuestaRepositorio respuestaRepositorio;

            using (_contexto = new ALICORPContexto(true))
            {
                try
                {
                    _repositorio = new VerificacionRepositorio(_contexto.Connection, _contexto.Transaction);
                    categoriaRepositorio = new CategoriaRepositorio(_contexto.Connection, _contexto.Transaction);
                    preguntaRepositorio = new PreguntaRepositorio(_contexto.Connection, _contexto.Transaction);
                    respuestaRepositorio = new RespuestaRepositorio(_contexto.Connection, _contexto.Transaction);

                    if (_repositorio.Guardar(entidad) && entidad.Categorias != null)
                    {
                        foreach (var item in entidad.Categorias)
                        {
                            item.VerificacionId = entidad.Id;
                            if (categoriaRepositorio.Guardar(item) && item.Preguntas != null)
                            {
                                foreach (var subItem in item.Preguntas)
                                {
                                    subItem.CategoriaId = item.Id;
                                    if (preguntaRepositorio.Guardar(subItem) && subItem.Respuestas != null)
                                    {
                                        foreach (var subItem2 in subItem.Respuestas)
                                        {
                                            subItem2.PreguntaId = subItem.Id;
                                            respuestaRepositorio.Guardar(subItem2);
                                        }
                                    }
                                }
                            }
                        }
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

        public bool Actualizar(Verificacion entidad)
        {
            CategoriaRepositorio categoriaRepositorio;
            PreguntaRepositorio preguntaRepositorio;
            RespuestaRepositorio respuestaRepositorio;

            using (_contexto = new ALICORPContexto(true))
            {
                bool respuesta = false;
                try
                {
                    _repositorio = new VerificacionRepositorio(_contexto.Connection, _contexto.Transaction);
                    categoriaRepositorio = new CategoriaRepositorio(_contexto.Connection, _contexto.Transaction);
                    preguntaRepositorio = new PreguntaRepositorio(_contexto.Connection, _contexto.Transaction);
                    respuestaRepositorio = new RespuestaRepositorio(_contexto.Connection, _contexto.Transaction);

                    if (_repositorio.Actualizar(entidad) && entidad.Categorias != null)
                    {
                        foreach (var item in entidad.Categorias)
                        {
                            if (item.Id > 0) respuesta = categoriaRepositorio.Actualizar(item);
                            else
                            {
                                item.VerificacionId = entidad.Id;
                                respuesta = categoriaRepositorio.Guardar(item);
                            }
                            if (respuesta && item.Preguntas != null)
                            {
                                foreach (var subItem in item.Preguntas)
                                {
                                    if (subItem.Id > 0) respuesta = preguntaRepositorio.Actualizar(subItem);
                                    else
                                    {
                                        subItem.CategoriaId = item.Id;
                                        respuesta = preguntaRepositorio.Guardar(subItem);
                                    }

                                    if (respuesta && subItem.Respuestas != null)
                                    {
                                        respuestaRepositorio.Limpiar(subItem.Id);
                                        foreach (var subItem2 in subItem.Respuestas)
                                        {
                                            subItem2.PreguntaId = subItem.Id;
                                            respuestaRepositorio.Guardar(subItem2);
                                        }
                                    }
                                }
                            }
                        }
                    }

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
