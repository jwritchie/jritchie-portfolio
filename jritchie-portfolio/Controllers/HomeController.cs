﻿using jritchie_portfolio.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace jritchie_portfolio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p>Email From: <bold>{0}</bold>({1})</p><p>Message:</p><p>{2}</p>";
                    var from = "MyPortfolio<jritchie.nc@gmail.com>";
                    //model.Body = "This is a message from your portfolio site.  The name and the email of the contacting person is above.";
                    string subject = null;
                    if (model.Subject != null)
                    {
                        subject = "Portfolio Contact Email: " + model.Subject;
                    }
                    else
                    {
                        subject = "Portfolio Contact Email";
                    }


                    var email = new MailMessage(from, ConfigurationManager.AppSettings["emailto"])
                    {
                        //Subject = "Portfolio Contact Email" + model.Subject,
                        Subject = subject,
                        Body = string.Format(body, model.FromName, model.FromEmail, model.Body),
                        IsBodyHtml = true
                    };

                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);             // Sends email.

                    return View(new EmailModel());          // Return to View (need to send empty Email model)
                    //return RedirectToAction("Sent");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }

        public ActionResult Portfolio()
        {
            return View();
        }

        public ActionResult TabPrinter()
        {
            return View();
        }

        public ActionResult GTCCCapstone()
        {
            return View();
        }

        public ActionResult Resume()
        {
            return View();
        }

        public FileResult JRitchieResume(string filename)
        {
            return File(filename, "application/pdf");
        }
    }
}