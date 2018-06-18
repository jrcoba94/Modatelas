using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;
using System.Data;
using System.Net;
using PagedList;
using Servicios.Model;

namespace ProyectoTelas.Controllers
{
    public class ProductoController : Controller
    {
        private TelasEntities db = new TelasEntities();
        public string UploadDirectory = "";

        // GET: Producto
        public ActionResult Index(int? page)
        {
            return View(db.Producto.ToList().ToPagedList(page ?? 1,5));
            //return View();
        }

        #region Método que se encarga de dar de alta a un nuevo Producto

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categoria, "CategoriaId", "Nombre");
            return View("Create");
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto producto, HttpPostedFileBase files, HttpPostedFileBase[] carrousel)
        {
            if (ModelState.IsValid)
            {

                UploadDirectory = ProyectoTelas.Properties.Settings.Default.DirectorioImagenes;
                //string servername = Dashboard1._2.Properties.Settings.Default.NombreServidor;
                HttpPostedFileBase file = Request.Files["files"];
                var directorio = UploadDirectory;
                string pathRandom = Path.GetRandomFileName().Replace(/*'.', '-'*/"~/", "");
                string resultFileName = pathRandom + '_' + file.FileName.Replace("~/", "");
                string resultFileUrl = directorio + resultFileName;
                string resultFilePath = System.Web.HttpContext.Current.Request.MapPath(resultFileUrl);

                bool hasFile = false;
                ImagenProducto oImgProduct = null;

                var validate = db.Producto.Select(x => x.Nombre == producto.Nombre).First();
                if (!validate)
                {
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {
                        file.SaveAs(resultFilePath);
                        producto.ImagenPortada = /*servername +*/ resultFileUrl.Replace("~/", "");

                        oImgProduct = new ImagenProducto();
                        //oImgProduct.Tipo = file.ContentType;
                        oImgProduct.Url = producto.ImagenPortada.Replace("~/", "");
                        //oImgProduct.Estatus = true;

                        hasFile = true;
                    }

                    //producto.Estatus = true;
                    db.Producto.Add(producto);
                    db.SaveChanges();

                    oImgProduct.ProductoID = producto.ProductoID;
                    db.ImagenProducto.Add(oImgProduct);
                    db.SaveChanges();

                    if (hasFile)
                    {
                        oImgProduct.ProductoID = producto.ProductoID;
                        db.SaveChanges();
                    }


                    //}

                    return RedirectToAction("Index");
                }
            }

            ViewBag.CategoriaId = new SelectList(db.Categoria, "CategoriaId", "Nombre", producto.CategoriaID);
            return View(producto);

        }

        #endregion


        #region Método que se encarga de editar la información de un Producto

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categoria, "CategoriaId", "Nombre", producto.CategoriaID);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductoId,Codigo,Nombre,DescripcionCorta,Descripcion,CertificacionesInternacionales,DimensionMateriales,CaracteristicaDesempeño,Especificaciones,PrecioMinorista,Maximomuestra,Estatus,CategoriaId,UrlProducto,ImagenPortada")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categoria, "CategoriaId", "Nombre", producto.CategoriaID);
            return View(producto);
        }

        #endregion


        #region Método que se encarga de dar de baja a un registro

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Producto producto = db.Producto.Find(id);
            //producto.Estatus = false;
            db.Entry(producto).State = EntityState.Modified;
            //db.Producto.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}