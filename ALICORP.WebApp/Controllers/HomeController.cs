using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALICORP.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopMenu()
        {
            return PartialView("_TopMenu");
        }

        public ActionResult LeftMenu()
        {
            return PartialView("_LeftMenu");
        }
    }
}