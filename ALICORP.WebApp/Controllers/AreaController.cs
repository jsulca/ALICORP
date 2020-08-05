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
    public class AreaController : Controller
    {
        private AreaLogica _areaLogica;

        #region Acciones

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nuevo(string callBack = "SetArea")
        {
            ViewBag.CallBack = callBack;

            Area model = new Area { };
            return PartialView("_Nuevo", model);
        }

        [HttpPost]
        public ActionResult Nuevo(Area model)
        {
            try
            {
                Validar(model);
                if (ModelState.IsValid)
                {
                    _areaLogica = new AreaLogica();
                    _areaLogica.Guardar(model);
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

        public ActionResult Editar(int id, string callBack = "SetArea")
        {
            try
            {
                ViewBag.CallBack = callBack;

                _areaLogica = new AreaLogica();
                Area model = _areaLogica.Buscar(id);
                
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
        public ActionResult Editar(Area model)
        {
            try
            {
                Validar(model);
                if (ModelState.IsValid)
                {
                    _areaLogica = new AreaLogica();
                    _areaLogica.Actualizar(model);
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
                _areaLogica = new AreaLogica();
                List<Area> lista = _areaLogica.Listar() ?? new List<Area>();
                string respuesta = JsonConvert.SerializeObject(lista.Select(x => new
                {
                    x.Id,
                    x.Codigo,
                    x.Descripcion
                }));
                return Content(respuesta, "application/json");
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

        private void Validar(Area model)
        {
            ModelState.Clear();
            if (string.IsNullOrWhiteSpace(model.Descripcion)) ModelState.AddModelError("Descripcion", "Ingrese una descripción.");
        }

        #endregion
    }
}