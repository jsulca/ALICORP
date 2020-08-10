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
    public class ColorController : Controller
    {
        private ColorLogica _colorLogica;

        #region Acciones

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nuevo(string callBack = "SetColor")
        {
            ViewBag.CallBack = callBack;

            Color model = new Color { };
            return PartialView("_Nuevo", model);
        }

        [HttpPost]
        public ActionResult Nuevo(Color model)
        {
            try
            {
                Validar(model);
                if (ModelState.IsValid)
                {
                    _colorLogica = new ColorLogica();
                    _colorLogica.Guardar(model);
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

        public ActionResult Editar(int id, string callBack = "SetColor")
        {
            try
            {
                ViewBag.CallBack = callBack;

                _colorLogica = new ColorLogica();
                Color model = _colorLogica.Buscar(id);

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
        public ActionResult Editar(Color model)
        {
            try
            {
                Validar(model);
                if (ModelState.IsValid)
                {
                    _colorLogica = new ColorLogica();
                    _colorLogica.Actualizar(model);
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
                _colorLogica = new ColorLogica();
                List<Color> lista = _colorLogica.Listar() ?? new List<Color>();
                string respuesta = JsonConvert.SerializeObject(lista.Select(x => new
                {
                    x.Id,
                    x.Descripcion,
                    x.Hex,
                    x.Rgba,
                    x.Clase
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

        private void Validar(Color model)
        {
            ModelState.Clear();
            if (string.IsNullOrWhiteSpace(model.Descripcion)) ModelState.AddModelError("Descripcion", "Ingrese una descripción.");
        }
        #endregion
    }
}