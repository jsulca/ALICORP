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
    public class EmpleadoController : Controller
    {
        private EmpleadoLogica _empleadoLogica;

        #region Acciones
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nuevo(string callBack = "SetEmpleado")
        {
            try
            {
                _empleadoLogica = new EmpleadoLogica();

                List<Cargo> cargos = null;
                List<Area> areas = null;

                _empleadoLogica.ObtenerParametros(ref cargos, ref areas);

                ViewBag.Cargos = cargos ?? new List<Cargo>();
                ViewBag.Areas = areas ?? new List<Area>();

                ViewBag.CallBack = callBack;
                Empleado model = new Empleado {  };

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
        public ActionResult Nuevo(Empleado model)
        {
            try
            {
                Validar(model);
                if (ModelState.IsValid)
                {
                    _empleadoLogica = new EmpleadoLogica();
                    _empleadoLogica.Guardar(model);
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

        public ActionResult Editar(int id, string callBack = "SetEmpleado")
        {
            try
            {
                _empleadoLogica = new EmpleadoLogica();

                List<Cargo> cargos = null;
                List<Area> areas = null;

                _empleadoLogica.ObtenerParametros(ref cargos, ref areas);

                ViewBag.Cargos = cargos ?? new List<Cargo>();
                ViewBag.Areas = areas ?? new List<Area>();

                ViewBag.CallBack = callBack;

                _empleadoLogica = new EmpleadoLogica();
                Empleado model = _empleadoLogica.Buscar(id);

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
        public ActionResult Editar(Empleado model)
        {
            try
            {
                Validar(model);
                if (ModelState.IsValid)
                {
                    _empleadoLogica = new EmpleadoLogica();
                    _empleadoLogica.Actualizar(model);
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

        public ActionResult Masivo()
        {
            try
            {
                _empleadoLogica = new EmpleadoLogica();

                List<Cargo> cargos = null;
                List<Area> areas = null;

                _empleadoLogica.ObtenerParametros(ref cargos, ref areas);

                ViewBag.Cargos = cargos ?? new List<Cargo>();
                ViewBag.Areas = areas ?? new List<Area>();

                return View();
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
                _empleadoLogica = new EmpleadoLogica();
                List<Empleado> lista = _empleadoLogica.Listar() ?? new List<Empleado>();
                string respuesta = JsonConvert.SerializeObject(lista.Select(x => new
                {
                    x.Id,
                    x.CargoId,
                    x.AreaId,
                    x.Nombre,
                    x.ApellidoPaterno,
                    x.ApellidoMaterno,
                    x.NroDocumento,
                    x.Correo,
                    x.Telefono,
                    Cargo = new { x.Cargo?.Descripcion },
                    Area = new { x.Area?.Descripcion }
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

        private void Validar(Empleado model)
        {
            ModelState.Clear();
            if (string.IsNullOrWhiteSpace(model.Nombre)) ModelState.AddModelError("Nombre", "Ingrese un Nombre.");
            if (string.IsNullOrWhiteSpace(model.ApellidoPaterno)) ModelState.AddModelError("ApellidoPaterno", "Ingrese su apellido paterno.");
            if (string.IsNullOrWhiteSpace(model.ApellidoMaterno)) ModelState.AddModelError("ApellidoMaterno", "Ingrese su apellido materno.");
            if (string.IsNullOrWhiteSpace(model.NroDocumento)) ModelState.AddModelError("NroDocumento", "Ingrese su Nro de DNI.");
        }

        #endregion
    }
}