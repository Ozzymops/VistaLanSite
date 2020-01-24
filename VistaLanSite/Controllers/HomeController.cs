using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VistaLanSite.Models;
using VistaLanSite.Classes;

namespace VistaLanSite.Controllers
{
    public class HomeController : Controller
    {
        private int AvailableSpots = 100;

        public IActionResult Index()
        {
            Queries Database = new Queries();
            IndexModel Model = new IndexModel();
            Model.AvailableSpots = AvailableSpots - Database.RetrieveParticipantCount();

            return View(Model);
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
        public IActionResult SubmitRegistration(RegistrationModel Model)
        {
            Queries Database = new Queries();
            string ViewMessage = "";

            if (AvailableSpots - Database.RetrieveParticipantCount() <= 0)
            {
                ViewMessage = "Registratie mislukt: alle plekken zijn al bezet.";
            }
            else
            {
                ViewMessage = "Je bent succesvol geregistreerd voor de LAN party, vergeet niet te betalen nadat we contact met je opnemen. Veel plezier!";
                Database.RegisterParticipant(Model.Participant);
            }

            return RedirectToAction("RegistrationComplete", "Home", new RouteValueDictionary { { "ViewMessage", ViewMessage } });
        }

        public IActionResult RegistrationComplete(string ViewMessage)
        {
            if (String.IsNullOrEmpty(ViewMessage))
            {
                ViewMessage = "Nada. Geen bericht voor jou.";
            }

            ViewData["Message"] = ViewMessage;

            return View();
        }

        public IActionResult Overview(OverviewModel Model, int ModelExists, bool OnlyUnpaidParticipants)
        {
            // check temp. login

            Queries Database = new Queries();

            if (ModelExists == 0)
            {
                Model = new OverviewModel();
                Model.OnlyUnpaidParticipants = OnlyUnpaidParticipants;
            }

            Model.ParticipantList = Database.RetrieveParticipants(Model.OnlyUnpaidParticipants);

            return View(Model);
        }

        public IActionResult UpdateParticipantStatus(int UpdatedParticipantId, bool OnlyUnpaidParticipants)
        {
            // check temp. login

            Queries Database = new Queries();

            Database.UpdateParticipantStatus(UpdatedParticipantId);

            return RedirectToAction("Overview", "Home", new RouteValueDictionary { { "Model", null },  { "ModelExists", 0 }, { "OnlyUnpaidParticipants", OnlyUnpaidParticipants } });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
