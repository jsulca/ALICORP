﻿using ALICORP.Entidades;
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
    public class InstanciaController : Controller
    {
        private InstanciaLogica _instanciaLogica;
        private ColorLogica _colorLogica;

        #region Acciones

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nuevo(string callBack = "SetInstancia")
        {
            try
            {
                _colorLogica = new ColorLogica();
                ViewBag.Colores = _colorLogica.Listar() ?? new List<Color>();

                ViewBag.CallBack = callBack;

                Instancia model = new Instancia { };
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
        public ActionResult Nuevo(Instancia model)
        {
            try
            {
                Validar(model);
                if (ModelState.IsValid)
                {
                    _instanciaLogica = new InstanciaLogica();
                    _instanciaLogica.Guardar(model);
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

        public ActionResult Editar(int id, string callBack = "SetInstancia")
        {
            try
            {
                _colorLogica = new ColorLogica();
                ViewBag.Colores = _colorLogica.Listar() ?? new List<Color>();

                ViewBag.CallBack = callBack;

                _instanciaLogica = new InstanciaLogica();
                Instancia model = _instanciaLogica.Buscar(id);

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
        public ActionResult Editar(Instancia model)
        {
            try
            {
                Validar(model);
                if (ModelState.IsValid)
                {
                    _instanciaLogica = new InstanciaLogica();
                    _instanciaLogica.Actualizar(model);
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
                _instanciaLogica = new InstanciaLogica();
                List<Instancia> lista = _instanciaLogica.Listar() ?? new List<Instancia>();
                string respuesta = JsonConvert.SerializeObject(lista.Select(x => new
                {
                    x.Id,
                    x.Abreviatura,
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

        private void Validar(Instancia model)
        {
            ModelState.Clear();
            if (model.ColorFondoId <= 0) ModelState.AddModelError("ColorFondoId", "Seleccione un color de fondo.");
            if (model.ColorTextoId <= 0) ModelState.AddModelError("ColorTextoId", "Seleccione un color de texto.");
            if (string.IsNullOrWhiteSpace(model.Descripcion)) ModelState.AddModelError("Descripcion", "Ingrese una descripcion");
        }

        #endregion
    }
}