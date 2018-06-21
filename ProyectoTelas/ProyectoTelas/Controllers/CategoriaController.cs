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
using PagedList;

namespace ProyectoTelas.Controllers
{
    public class CategoriaController : Controller
    {
        #region Variables e instancias

        private Entities db = new Entities();
        public string UploadDirectory = "";
        SrvCategoria oSrvCategoria = new SrvCategoria();

        #endregion

        // GET: Categoria
        //public ActionResult Index()
        //{
        //    return View();
        //}

        #region Método que llama la vista Index para mostrar la lista de productos en el Table

        //public ActionResult Index()
        //{
        //    var Category = db.Categoria.ToList();
        //    return View(Category.ToList());
        //}

        public ActionResult Index(int? page)
        {
            return View(db.Categoria.ToList().ToPagedList(page ?? 1, 5));
        }

        #endregion

        #region Método para crear una nueva Categoria

        public ActionResult CreateCategory()
        {
            var model = oSrvCategoria.GetCategoria();

            //ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Nombre");
            return View("Crear");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(Categoria oCategoria, HttpPostedFileBase files)
        {
            var model = oSrvCategoria.GetCategoria();

            if (ModelState.IsValid)
            {
                db.Categoria.Add(oCategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Nombre", oCategoria);
            return View("Crear", oCategoria);
        }

        #endregion

        #region Método que permite modificar la información de una categoría

        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria oCategory = db.Categoria.Find(id);
            if (oCategory == null)
            {
                return HttpNotFound();
            }

            //ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Nombre", oCategory);
            return View("Editar", oCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(/*[Bind(Include = "ProductoId,Nombre,Descripcion,FechaInicio,FechaFin,ProductoId,PrecioPromocion,UrlImagen")]*/ Categoria oCategory, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Nombre", oCategory.CategoriaID);
            return View(oCategory);
        }

        #endregion

        #region Método que permite dar de baja a una categoría

        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria oCategory = db.Categoria.Find(id);
            if (oCategory == null)
            {
                return HttpNotFound();
            }
            return View("Eliminar", oCategory);
        }

        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id)
        {
            Categoria oCategory = db.Categoria.Find(id);
            db.Categoria.Remove(oCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}