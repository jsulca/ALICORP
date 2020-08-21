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
        private AreaLogica _areaLogica;
        private EstructuraAreaLogica _estructuraAreaLogica;
        private EstructuraInstanciaLogica _estructuraInstanciaLogica;

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
                _areaLogica = new AreaLogica();
                ViewBag.Areas = _areaLogica.Listar();

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
        public ActionResult Editar(Estructura model)
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
                    Estructura item = _estructuraLogica.Buscar(model.Id);
                    model.Tablero = item.Tablero;

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

        public ActionResult ModelarTablero(int id, string callBack = "SetModelarTablero")
        {
            try
            {
                _estructuraAreaLogica = new EstructuraAreaLogica();
                _estructuraInstanciaLogica = new EstructuraInstanciaLogica();

                Estructura model = new Estructura { Id = id };
                model.Areas = _estructuraAreaLogica.Listar(id) ?? new List<EstructuraArea>();
                model.Instancias = _estructuraInstanciaLogica.Listar(id) ?? new List<EstructuraInstancia>();

                _instanciaLogica = new InstanciaLogica();
                ViewBag.Instancias = _instanciaLogica.Listar() ?? new List<Instancia>();

                _areaLogica = new AreaLogica();
                ViewBag.Areas = _areaLogica.Listar() ?? new List<Area>();

                ViewBag.CallBack = callBack;

                return PartialView("_ModelarTablero", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ViewBag.Message = ex.Message;
                return PartialView("_Error");
            }
        }

        [HttpPost]
        public ActionResult ModelarTablero(Estructura model)
        {
            try
            {
                Validar(model.Instancias, model.Areas);
                if (ModelState.IsValid)
                {
                    if (model.Areas != null) model.Areas.ForEach(x => x.EstructuraId = model.Id);
                    if (model.Instancias != null) model.Instancias.ForEach(x => x.EstructuraId = model.Id);

                    _estructuraLogica = new EstructuraLogica();
                    _estructuraLogica.Guardar(model.Id, model.Instancias, model.Areas);

                    return Content("Se guardaron los cambios.");
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

                string rpta = JsonConvert.SerializeObject(lista.Select(x => new
                {
                    x.Id,
                    x.PadreId,
                    x.Codigo,
                    x.Descripcion,
                    x.Tablero
                }));
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
        private void Validar(Estructura model)
        {
            ModelState.Clear();
            if (string.IsNullOrWhiteSpace(model.Descripcion)) ModelState.AddModelError("Descripcion", "La descripción no puede estar vacio.");
        }

        [NonAction]
        private void Validar(List<EstructuraInstancia> instancias, List<EstructuraArea> areas)
        {
            ModelState.Clear();
            if (instancias != null)
                for (int i = 0; i < instancias.Count; i++)
                    if (instancias[i].InstanciaId <= 0) ModelState.AddModelError("Instancia_" + i, string.Format("La instancia {0} no tiene un identificador.", i + 1));

            if (areas != null)
                for (int i = 0; i < areas.Count; i++)
                    if (areas[i].AreaId <= 0) ModelState.AddModelError("Area_" + i, string.Format("El área {0} no tiene un identificador.", i + 1));
        }

        #endregion
    }
}