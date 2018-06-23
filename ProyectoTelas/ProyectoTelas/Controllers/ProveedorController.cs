using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicios.Model;
using Servicios.Servicios;
using PagedList;
using System.Net;
using System.Data.Entity;

namespace ProyectoTelas.Controllers
{
    public class ProveedorController : Controller
    {
        #region Variables e instancias

        private Entities db = new Entities();
        public string UploadDirectory = "";
        SrvProveedor oSrvProveedor = new SrvProveedor();

        #endregion
        // GET: Proveedor

        public ActionResult Detalles(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor oProveedor = db.Proveedor.Find(id);
            if (oProveedor == null)
            {
                return HttpNotFound();
            }
            return View("Detalle", oProveedor);
        }

        #region Método que llama la vista Index para mostrar la lista de productos en el Table

        //public ActionResult Index()
        //{
        //    var Category = db.Categoria.ToList();
        //    return View(Category.ToList());
        //}

        public ActionResult Index(int? page)
        {
            return View(db.Proveedor.ToList().ToPagedList(page ?? 1, 5));
        }

        #endregion

        #region Método para crear un nuevo Proveedor

        public ActionResult CreateProveedor()
        {
            var model = oSrvProveedor.GetProveedor();

            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Nombre");
            return View("Crear");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProveedor(Proveedor oProveedor, HttpPostedFileBase files)
        {
            var model = oSrvProveedor.GetProveedor();

            if (ModelState.IsValid)
            {
                db.Proveedor.Add(oProveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Nombre", oCategoria);
            return View("Crear", oProveedor);
        }

        #endregion

        #region Método que permite modificar la información de un Proveedor

        public ActionResult EditProveedor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor oProveedor = db.Proveedor.Find(id);
            if (oProveedor == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Nombre", oProveedor);
            return View("Editar", oProveedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProveedor(/*[Bind(Include = "ProductoId,Nombre,Descripcion,FechaInicio,FechaFin,ProductoId,PrecioPromocion,UrlImagen")]*/ Proveedor oProveedor, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oProveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Nombre");
            return View(oProveedor);
        }

        #endregion

        #region Método que permite dar de baja a una categoría

        public ActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor oProveedor = db.Proveedor.Find(id);
            if (oProveedor == null)
            {
                return HttpNotFound();
            }
            return View("Eliminar", oProveedor);
        }

        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public ActionResult Borrar(int id)
        {
            Proveedor oProveedor = db.Proveedor.Find(id);
            db.Proveedor.Remove(oProveedor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}