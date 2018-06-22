using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        [HttpGet]
        public ActionResult Contacto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contacto(/*string receiverEmail, string subject, string message*/ string nombre, string correo, string comentario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var senderemail = new MailAddress("iamfastherz@gmail.com", "Demo");
                    //var receiveremail = new MailAddress(receiverEmail, "Recibido");

                    //var password = "demo123";
                    //var sub = subject;
                    //var body = message;

                    //var smtp = new SmtpClient
                    //{
                    //    Host = "smtp.gmail.com",
                    //    Port = 587,
                    //    EnableSsl = true,
                    //    DeliveryMethod = SmtpDeliveryMethod.Network,
                    //    UseDefaultCredentials = false,
                    //    Credentials = new NetworkCredential(senderemail.Address, password)

                    //};

                    //using (var mess = new MailMessage(senderemail, receiveremail)
                    //{
                    //    Subject = subject,
                    //    Body = body
                    //})
                    //{
                    //    smtp.Send(mess);
                    //}

                    //return View();



                    MailMessage ocorreo = new MailMessage();
                    ocorreo.From = new MailAddress("thegreatfastherz@gmail.com"); //El correo electrónico que usará la aplicación MVC
                    ocorreo.To.Add(nombre);
                    ocorreo.Subject = correo;
                    ocorreo.Body = comentario;
                    ocorreo.IsBodyHtml = true;
                    ocorreo.Priority = MailPriority.Normal;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 25;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    string sCuentaCorreo = "thegreatfastherz@gmail.com";
                    string sPasswordCorreo = "Champion123";
                    smtp.Credentials = new System.Net.NetworkCredential(sCuentaCorreo, sPasswordCorreo);

                    smtp.Send(ocorreo);
                    ViewBag.Mensaje = "Se ha enviado su mensaje de manera correcta";
                }


                //MailMessage ocorreo = new MailMessage();
                //ocorreo.From = new MailAddress("iamfastherz@gmail.com"); //El correo electrónico que usará la aplicación MVC
                //ocorreo.To.Add(nombre);
                //ocorreo.Subject = correo;
                //ocorreo.Body = comentario;
                //ocorreo.IsBodyHtml = true;
                //ocorreo.Priority = MailPriority.Normal;

                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp.gmail.com";
                //smtp.Port = 25;
                //smtp.EnableSsl = true;
                //smtp.UseDefaultCredentials = true;
                //string sCuentaCorreo = "iamfastherz@gmail.com";
                //string sPasswordCorreo = "Champion12345";
                //smtp.Credentials = new System.Net.NetworkCredential(sCuentaCorreo, sPasswordCorreo);

                //smtp.Send(ocorreo);
                //ViewBag.Mensaje = "Se ha enviado su mensaje de manera correcta";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }
            return View();
        }
    }
}