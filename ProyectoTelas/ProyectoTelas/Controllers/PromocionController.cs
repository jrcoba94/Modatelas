using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicios.Model;
using Servicios.Servicios;
using System.IO;
using System.Net;
using System.Data.Entity;

namespace ProyectoTelas.Controllers
{
    public class PromocionController : Controller
    {
        #region Variables e Instancias

        private TelasEntities db = new TelasEntities();
        public string UploadDirectory = "";
        SrvPromociones oSrvPromotions = new SrvPromociones();

        #region Método que llama a la vista Index
        public ActionResult Index()
        {
            var promotion = db.Promocion.Include(x => x.Producto);
            return View(promotion.ToList());
        }
        #endregion

        #endregion

        // GET: Promocion
        //public ActionResult Index()
        //{
        //    return View();
        //}

        #region Método que da de alta a una nueva Promoción

        public ActionResult CreatePromotion()
        {
            //var model = oSrvPromotions.getPromotions();

            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoId", "Nombre");
            return View("CreatePromotions");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePromotion(Promocion oPromotion, HttpPostedFileBase files)
        {
            var model = oSrvPromotions.GetPromocion();

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

                var validate = db.Promocion.Select(x => x.Nombre == oPromotion.Nombre).First();
                if (!validate)
                {
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {
                        file.SaveAs(resultFilePath);
                        oPromotion.UrlImagen = /*servername +*/ resultFileUrl.Replace("~/", "");

                        oImgProduct = new ImagenProducto();
                        //oImgProduct.Tipo = file.ContentType;
                        oImgProduct.Url = oPromotion.UrlImagen.Replace("~/", "");
                        //oImgProduct.Estatus = true;
                        hasFile = true;
                    }

                    db.Promocion.Add(oPromotion);
                    db.SaveChanges();

                    if (hasFile)
                    {
                        oImgProduct.ProductoID = oPromotion.PromocionID;
                        db.SaveChanges();
                    }
                    //here the notification is created
                    return RedirectToAction("Index");
                }
            }

            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoId", "Nombre", oPromotion.ProductoID);
            return View("CreatePromotions", oPromotion);
            //return View(oPromotion);
        }


        #endregion

        #region Método que actualiza la información de una Promoción

        public ActionResult EditPromotion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promocion oPromotion = db.Promocion.Find(id);
            if (oPromotion == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Nombre", oPromotion.ProductoID);
            return View("DetailsPromotions", oPromotion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPromotion(/*[Bind(Include = "ProductoId,Nombre,Descripcion,FechaInicio,FechaFin,ProductoId,PrecioPromocion,UrlImagen")]*/ Promocion oPromocion, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oPromocion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Nombre", oPromocion.ProductoID);
            return View(oPromocion);
        }

        #endregion

        #region Método que se utiliza para dar de baja a una Promoción

        public ActionResult DeletePromotion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promocion oPromotion = db.Promocion.Find(id);
            if (oPromotion == null)
            {
                return HttpNotFound();
            }
            return View("DeletePromotions", oPromotion);
        }

        [HttpPost, ActionName("DeletePromotion")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promocion oPromocion = db.Promocion.Find(id);
            db.Promocion.Remove(oPromocion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}