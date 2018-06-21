using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoTelas.Controllers
{
    public class SitioController : Controller
    {
        // GET: Sitio
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}