using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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

            return RedirectToAction("RegistrationComplete", "Home");
        }

        public IActionResult RegistrationComplete()
        {
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
