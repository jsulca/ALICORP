using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALICORP.WebApp.Controllers
{
    public class CompromisoController : Controller
    {
        #region Acciones
        
        //Mis Compromisos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nuevo()
        {
            return PartialView("_Nuevo");
        }

        public ActionResult Administracion()
        {
            return View();
        }

        public  ActionResult Tablero()
        {
            return View();
        }

        #endregion


    }
}