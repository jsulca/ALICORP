using ALICORP.Entidades;
using ALICORP.Logicas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ALICORP.WebApp.Controllers
{
    public class EstructuraController : Controller
    {
        private EstructuraLogica _estructuraLogica;
        private InstanciaLogica _instanciaLogica;

        #region Acciones

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nuevo(int? padreId, string callBack = "SetEstructura")
        {
            Estructura model = new Estructura { PadreId = padreId };
            try
            {
                _instanciaLogica = new InstanciaLogica();
                ViewBag.Instancias = _instanciaLogica.Listar() ?? new List<Instancia>();

                ViewBag.CallBack = callBack;
                return PartialView("_Nuevo", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Nuevo(Estructura model)
        {
            try
            {
                _estructuraLogica = new EstructuraLogica();

                Validar(model);
                if (ModelState.IsValid)
                {
                    if (model.Tablero && model.PadreId.HasValue && _estructuraLogica.TieneTablero(model.PadreId.Value)) 
                        ModelState.AddModelError("Tablero", "No puede agregar un tablero dentro de otro.");
                }
                if (ModelState.IsValid)
                {
                    _estructuraLogica.Guardar(model);
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

        public ActionResult Editar(int id, string callBack = "SetEstructura")
        {
            try
            {
                _estructuraLogica = new EstructuraLogica();
                Estructura model = _estructuraLogica.Buscar(id);
                ViewBag.CallBack = callBack;

                _instanciaLogica = new InstanciaLogica();
                ViewBag.Instancias = _instanciaLogica.Listar() ?? new List<Instancia>();

                return PartialView("_Editar", model);
            }
            catch ( Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.Message = ex.Message;
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult Editar(Estructura model)
        {
            try
            {
                Validar(model);
                if (ModelState.IsValid)
                {
                    _estructuraLogica = new EstructuraLogica();
                    _estructuraLogica.Actualizar(model);
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

        public ActionResult Listar()
        {
            try
            {
                _estructuraLogica = new EstructuraLogica();
                List<Estructura> lista = _estructuraLogica.Listar();

                string rpta = JsonConvert.SerializeObject(lista.Select(x => new {
                    x.Id,
                    x.PadreId,
                    x.Codigo,
                    x.Descripcion,
                    x.InstanciaId,
                    x.Instancia,
                    x.Tablero
                }));
                return Content(rpta);
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
        private void Validar(Estructura model)
        {
            ModelState.Clear();
            if (string.IsNullOrWhiteSpace(model.Descripcion)) ModelState.AddModelError("Descripcion", "La descripción no puede estar vacio.");
        }

        #endregion
    }
}