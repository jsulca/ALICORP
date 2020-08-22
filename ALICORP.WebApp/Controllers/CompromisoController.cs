using ALICORP.Entidades;
using ALICORP.Entidades.Filtros;
using ALICORP.Logicas;
using ALICORP.WebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;

namespace ALICORP.WebApp.Controllers
{
    public class CompromisoController : Controller
    {
        private EstructuraLogica _estructuraLogica;
        private CompromisoLogica _compromisoLogica;

        private static readonly int _estructuraId = 16;

        #region Acciones

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nuevo(string callBack = "SetCompromiso")
        {
            try
            {
                _estructuraLogica = new EstructuraLogica();

                Compromiso model = new Compromiso { EstructuraId = _estructuraId };

                ViewBag.Tableros = _estructuraLogica.Listar(new EstructuraFiltro { Tablero = true }) ?? new List<Estructura>();
                ViewBag.Ruta = string.Concat("/ALICORP", _estructuraLogica.Ruta(_estructuraId)?.ToUpper() ?? "");
                ViewBag.CallBack = callBack;

                return PartialView("_Nuevo", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.Message = ex.Message;
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult Nuevo(CompromisoModel.Nuevo model)
        {
            try
            {
                Validar(model);
                if (ModelState.IsValid)
                {
                    Compromiso entidad = model.Get();
                    entidad.EstructuraId = _estructuraId;
                    entidad.Estado = EstadoCompromiso.NUEVO;

                    _compromisoLogica = new CompromisoLogica();
                    _compromisoLogica.Guardar(entidad);

                    return Content(entidad.Id.ToString());
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Error");
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.Message = ex.Message;
                return PartialView("_Error");
            }
        }

        public ActionResult Editar(int id, string callBack = "SetCompromiso")
        {
            try
            {
                _estructuraLogica = new EstructuraLogica();
                _compromisoLogica = new CompromisoLogica();

                Compromiso model = _compromisoLogica.Buscar(id);

                ViewBag.Tablero = string.Concat("/ALICORP", _estructuraLogica.Ruta(model.TableroId)?.ToUpper() ?? "");
                ViewBag.CallBack = callBack;

                return PartialView("_Editar", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.Message = ex.Message;
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult Editar(CompromisoModel.Editar model)
        {
            try
            {
                Validar(model);
                if (ModelState.IsValid)
                {
                    Compromiso entidad = model.Get();

                    _compromisoLogica = new CompromisoLogica();
                    _compromisoLogica.Actualizar(entidad);
                    return Content(model.Id.ToString());
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Error");
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.Message = ex.Message;
                return PartialView("_Error");
            }
        }

        public ActionResult Seguimiento(int id)
        {
            try
            {
                _compromisoLogica = new CompromisoLogica();
                _estructuraLogica = new EstructuraLogica();

                Compromiso model = _compromisoLogica.Buscar(id, true);
                ViewBag.Ruta = string.Concat("/ALICORP", _estructuraLogica.Ruta(model.EstructuraId)?.ToUpper() ?? "");

                return PartialView("_Seguimiento", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.Message = ex.Message;
                return PartialView("_Error");
            }
        }

        public ActionResult Verificacion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verificacion(CompromisoModel.Verificar model)
        {
            try
            {
                Validar(model);
                if (ModelState.IsValid)
                {
                    _compromisoLogica = new CompromisoLogica();
                    _compromisoLogica.Finalizar(model.Id.Value, model.Respuesta);
                    return Content(model.Id.Value.ToString());
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Error");
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.Message = ex.Message;
                return PartialView("_Error");
            }
        }

        public ActionResult Administracion()
        {
            return View();
        }

        public ActionResult Tablero()
        {
            try
            {
                _estructuraLogica = new EstructuraLogica();
                List<Estructura> tableros = _estructuraLogica.Listar(new EstructuraFiltro { Tablero = true }) ?? new List<Estructura>();
                return View(tableros);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.Message = ex.Message;
                return PartialView("_Error");
            }
        }

        public ActionResult Gestion()
        {
            try
            {
                List<Compromiso> compromisos = new List<Compromiso>();
                return View();
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.Message = ex.Message;
                return PartialView("_Error");
            }
        }

        public ActionResult Ver(int id, bool parcial = true)
        {
            try
            {
                _compromisoLogica = new CompromisoLogica();
                _estructuraLogica = new EstructuraLogica();

                Compromiso model = _compromisoLogica.Buscar(id);

                ViewBag.Tablero = string.Concat("/ALICORP", _estructuraLogica.Ruta(model.TableroId)?.ToUpper() ?? "");
                ViewBag.Ruta = string.Concat("/ALICORP", _estructuraLogica.Ruta(model.EstructuraId)?.ToUpper() ?? "");

                if (parcial) return PartialView("_Ver", model);
                else return View(model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.Message = ex.Message;
                return PartialView("_Error");
            }
        }

        public ActionResult ListarPorPagina(CompromisoFiltro filter, int pageIndex, int pageSize)
        {
            try
            {
                _compromisoLogica = new CompromisoLogica();
                int totalRows = 0;
                List<Compromiso> lista = _compromisoLogica.ListarPorPagina(filter, pageIndex, pageSize, ref totalRows) ?? new List<Compromiso>();

                string rpta = JsonConvert.SerializeObject(new
                {
                    lista = lista.Select(x => new
                    {
                        x.Id,
                        x.Codigo,
                        x.Descripcion,
                        Fecha = x.FechaRegistro.ToString("dd/MM/yyyy HH:mm"),
                        Estado = x.Estado.ToString(),
                        Nuevo = x.FechaRegistro.Date == DateTime.Today,
                        Tablero = new { x.Tablero.Descripcion }
                    }),
                    totalRows
                });
                return Content(rpta, "application/json");
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.Message = ex.Message;
                return PartialView("_Error");
            }
        }

        #endregion

        #region Metodos y Funciones

        [NonAction]
        private void Validar(CompromisoModel.Editar model)
        {
            ModelState.Clear();
            if(!model.Id.HasValue || model.Id.Value <= 0) ModelState.AddModelError("Id", "El compromiso no tiene un identificador.");
            if (string.IsNullOrWhiteSpace(model.Descripcion)) ModelState.AddModelError("Descripcion", "Es necesario ingresar una breve descripción.");
        }

        [NonAction]
        private void Validar(CompromisoModel.Nuevo model)
        {
            ModelState.Clear();
            if(!model.TableroId.HasValue || model.TableroId.Value <= 0) ModelState.AddModelError("TableroId", "Es necesario seleccionar el tablero.");
            if (string.IsNullOrWhiteSpace(model.Descripcion)) ModelState.AddModelError("Descripcion", "Es necesario ingresar una breve descripción.");
        }

        [NonAction]
        private void Validar(CompromisoModel.Verificar model)
        {
            ModelState.Clear();
            if (!model.Id.HasValue || model.Id.Value <= 0) ModelState.AddModelError("Id", "El compromiso no tiene un identificador.");
            if (string.IsNullOrWhiteSpace(model.Respuesta)) ModelState.AddModelError("Respuesta", "Es necesario ingresar respuesta.");
        }

        #endregion
    }
}