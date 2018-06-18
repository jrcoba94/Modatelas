using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicios.Model;
using Servicios.Servicios;
using System.Net;
using System.Data.Entity;
using System.IO;

namespace ProyectoTelas.Controllers
{
    public class CategoriaController : Controller
    {

        // GET: Categoria
        public ActionResult Index()
        {
            return View();
        }
    }
}