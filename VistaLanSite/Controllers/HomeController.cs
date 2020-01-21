using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VistaLanSite.Models;

namespace VistaLanSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitRegistration(Participant NewParticipant)
        {
            Queries Database = new Queries();
            Database.RegisterParticipant(NewParticipant);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Overview()
        {
            // check temp. login

            Queries Database = new Queries();
            OverviewModel Model = new OverviewModel();
            Model.ParticipantList = Database.RetrieveParticipants();

            return View(Model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
