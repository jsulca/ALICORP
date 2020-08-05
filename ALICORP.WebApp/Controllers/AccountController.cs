using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALICORP.WebApp.Controllers
{
    public class AccountController : Controller
    {
        #region Acciones

        public ActionResult Login()
        {
            return View();
        }

        public  ActionResult Logout()
        {
            return RedirectToAction("Login");
        }

        #endregion
    }
}