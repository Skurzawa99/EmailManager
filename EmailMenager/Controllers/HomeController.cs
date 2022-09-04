using EmailMenager.Models.Domains;
using EmailMenager.Models.Repositories;
using EmailMenager.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace EmailMenager.Controllers
{
    public class HomeController : Controller
    {
        private EmailRepository _emailRepository = new EmailRepository();
        private ConfigRepository _configRepository = new ConfigRepository();
        private StringCipher _stringCipher = new StringCipher("47665616-D94B-4A26-BD1C-E1F1C13E3E61");

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Email email, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
                return View("Index", email);

            var config = _configRepository.GetConfig();

            if (config == null)
            {
                ViewBag.error = 1;
                return View("Index");
            }
            email.Config = config;    

            MailMessage _mail = new MailMessage();
            _mail.From = new MailAddress(config.Email, email.SenderName);

            string[] Multi = email.ReciverEmail.Split(';');
            foreach (string s in Multi) 
            {
                _mail.To.Add(new MailAddress(s));
            }
            
            _mail.Subject = email.Subject;
            _mail.Body = email.Body;

            if (upload != null)
            {
                email.FileName = Path.GetFileName(upload.FileName);
                _mail.Attachments.Add(new Attachment(upload.InputStream, email.FileName));
                email.FileUpload = new byte[upload.InputStream.Length];
                upload.InputStream.Read(email.FileUpload, 0, email.FileUpload.Length);
            }
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = config.Host;
                smtp.Port = config.Port;
                smtp.EnableSsl = config.EnableSsl;
                NetworkCredential nc = new NetworkCredential(config.Email, _stringCipher.Decrypt(config.PasswordEmail));
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;
                smtp.Send(_mail);
            }
            catch (Exception)
            {
                ViewBag.error = 2;
                return View("Index");
            }
            
            _emailRepository.Add(email);

            return RedirectToAction("History");
        }

        public ActionResult Config()
        {
            var config = _configRepository.GetConfig();
            if (config == null)
                ViewBag.configError = 1;

            return View(config);           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Config(Config config)
        {
            if (!ModelState.IsValid)
                return View("Config", config);

            config.PasswordEmail = _stringCipher.Encrypt(config.PasswordEmail);
            _configRepository.Add(config);

            return RedirectToAction("Config");
        }

        public ActionResult History()
        {
            var emails = _emailRepository.GetEmails();

            return View(emails);
        }

        public ActionResult DownloadFile(int id)
        {
            var email = _emailRepository.GetEmail(id);

            return File(email.FileUpload, "application/pdf", email.FileName);
        }

        public ActionResult Email(int id = 0)
        {
            if (id == 0)
                return View();
            
            var email = _emailRepository.GetEmail(id);
            return View(email);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _emailRepository.Delete(id);
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true });
        }

    }
}