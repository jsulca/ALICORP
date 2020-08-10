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
    public class CargoController : Controller
    {
        private CargoLogica _cargoLogica;

        #region Acciones

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nuevo(string callBack = "SetCargo")
        {
            ViewBag.CallBack = callBack;

            Cargo model = new Cargo { };
            return PartialView("_Nuevo", model);
        }

        [HttpPost]
        public ActionResult Nuevo(Cargo model)
        {
            try
            {
                Validar(model);
                if (ModelState.IsValid)
                {
                    _cargoLogica = new CargoLogica();
                    _cargoLogica.Guardar(model);
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

        public ActionResult Editar(int id, string callBack = "SetCargo")
        {
            try
            {
                ViewBag.CallBack = callBack;

                _cargoLogica = new CargoLogica();
                Cargo model = _cargoLogica.Buscar(id);

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
        public ActionResult Editar(Cargo model)
        {
            try
            {
                Validar(model);
                if (ModelState.IsValid)
                {
                    _cargoLogica = new CargoLogica();
                    _cargoLogica.Actualizar(model);
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
                _cargoLogica = new CargoLogica();
                List<Cargo> lista = _cargoLogica.Listar() ?? new List<Cargo>();
                string respuesta = JsonConvert.SerializeObject(lista.Select(x => new
                {
                    x.Id,
                    x.Codigo,
                    x.Descripcion,
                    x.Activo
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

        private void Validar(Cargo model)
        {
            ModelState.Clear();
            if (string.IsNullOrWhiteSpace(model.Descripcion)) ModelState.AddModelError("Descripcion", "Ingrese una descripción.");
        }

        #endregion
    }
}