﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Servicios.Model;
using Servicios.Servicios;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using PagedList;

namespace ProyectoTelas.Controllers
{
    public class SitioController : Controller
    {
        #region Instancias y Variables
        SrvProveedor oSrvProveedor = new SrvProveedor();
        SrvProducto oSrvProducto = new SrvProducto();
        #endregion

        // GET: Sitio
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Nosotros()
        {
            return View("Nosotros");
        }

        public ActionResult Mision()
        {
            return View("Mision");
        }

        public ActionResult Vision()
        {
            return View("Vision");
        }

        public ActionResult Contactanos()
        {
            return View("Contacto");
        }

        public ActionResult Productos()
        {
            var model = oSrvProducto.GetProducto();
            return View("Productos", model);
        }

        public ActionResult get_Productos()
        {
            List<Producto> model;
            Object respuesta = new Object();
            try
            {
                model = oSrvProducto.GetProducto();
                respuesta = new { accion = true, msj = "", urlImage = model.Select(x => x.ImagenPortada), Tipo = "Mensaje a Cliente" };
            }
            catch (ValidationException)
            {
                respuesta = new { action = false, msj = "", urlImage = "", Tipo = "Mensaje a Cliente" };
            }
            return Json(new JavaScriptSerializer().Serialize(respuesta), JsonRequestBehavior.AllowGet);
        }
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string TipoObra(string clase, string subclase, string genero, string subgenero)
        //{
        //    srvLeads srv = new srvLeads();
        //    // Return JSON data
        //    List<EntTipoObra> data = srv.getTipoObra(clase, subclase, genero, subgenero);
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    string retJSON = js.Serialize(data);
        //    return retJSON;
        //}

        [HttpPost]
        public ActionResult EnviarCorreo(Contacto oContacto, string nombre, string correo, string comentario)
        {
            var model = oSrvProveedor.getContacto();

            if (ModelState.IsValid)
            {
                using (Entities db = new Entities())
                {
                    oContacto.CorreoElectronico = correo;
                    db.Contacto.Add(oContacto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Contacto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contacto(string receiverEmail, string subject, string message)
        {
            try
            {
                MailMessage co = new MailMessage();
                co.From = new MailAddress(receiverEmail);
                co.To.Add("mexicanastelas@gmail.com");
                co.Subject = subject;
                co.SubjectEncoding = System.Text.Encoding.UTF8;
                co.Body = message;
                co.BodyEncoding = System.Text.Encoding.UTF8;
                co.IsBodyHtml = true;
                co.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential("mexicanastelas@gmail.com", "TELASmex2018");
                smtp.Send(co);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }
            return View();
        }
    }
}