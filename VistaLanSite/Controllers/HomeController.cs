using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using VistaLanSite.Models;
using VistaLanSite.Classes;
using Microsoft.AspNetCore.Http;

namespace VistaLanSite.Controllers
{
    public class HomeController : Controller
    {
        private int AvailableSpots = 100;

        #region Info pages
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

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Info()
        {
            Queries Database = new Queries();
            IndexModel Model = new IndexModel();
            Model.AvailableSpots = AvailableSpots - Database.RetrieveParticipantCount();

            return View(Model);
        }

        public IActionResult Sponsors()
        {
            return View();
        }

        public IActionResult Rules()
        {
            return View();
        }
        #endregion

        #region Registration actions
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
                ViewMessage = "Je bent succesvol geregistreerd voor de LAN-party, vergeet niet te betalen nadat we contact met je opnemen. Alvast veel plezier!";
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
        #endregion

        #region Administration actions
        public IActionResult Login(string ViewMessage)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                if (HttpContext.Session.GetString("User") == "Administratie:8MmNS")
                {
                    return RedirectToAction("Overview", "Home", new RouteValueDictionary { { "Model", null }, { "ModelExists", 0 }, { "OnlyUnpaidParticipants", false } });
                }
            }

            if (!String.IsNullOrEmpty(ViewMessage))
            {
                ViewData["Message"] = ViewMessage;
            }

            return View();
        }

        public IActionResult CheckLogin(LoginModel Model)
        {
            if (Model.Username == "Administratie" && Model.Password == "8MmNS") // remember this!
            {
                HttpContext.Session.SetString("User", Model.Username + ":" + Model.Password);
                return RedirectToAction("Overview", "Home", new RouteValueDictionary { { "Model", null }, { "ModelExists", 0 }, { "OnlyUnpaidParticipants", false } });
            }

            return RedirectToAction("Login", "Home", new RouteValueDictionary { { "ViewMessage", "Fout gebruikersnaam/wachtwoord. Probeer opnieuw." } });
        }

        public IActionResult Overview(OverviewModel Model, int ModelExists, bool OnlyUnpaidParticipants)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                if (HttpContext.Session.GetString("User") == "Administratie:8MmNS")
                {
                    ViewData["Message"] = null;

                    Queries Database = new Queries();

                    if (ModelExists == 0)
                    {
                        Model = new OverviewModel();
                        Model.OnlyUnpaidParticipants = OnlyUnpaidParticipants;
                    }

                    Model.ParticipantList = Database.RetrieveParticipants(Model.OnlyUnpaidParticipants);

                    return View(Model);
                }
            }

            return RedirectToAction("Login", "Home", new RouteValueDictionary { { "ViewMessage", "Je bent niet gemachtigd om deze pagina te bezoeken." } });
        }

        public IActionResult UpdateParticipantStatus(int UpdatedParticipantId, bool OnlyUnpaidParticipants)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                if (HttpContext.Session.GetString("User") == "Administratie:8MmNS")
                {
                    ViewData["Message"] = null;

                    Queries Database = new Queries();

                    Database.UpdateParticipantStatus(UpdatedParticipantId);

                    return RedirectToAction("Overview", "Home", new RouteValueDictionary { { "Model", null }, { "ModelExists", 0 }, { "OnlyUnpaidParticipants", OnlyUnpaidParticipants } });
                }
            }

            return RedirectToAction("Login", "Home", new RouteValueDictionary { { "ViewMessage", "Je bent niet gemachtigd om deze actie uit te voeren." } });
        }

        public IActionResult DeleteParticipant(int DeletedParticipantId, bool OnlyUnpaidParticipants)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                if (HttpContext.Session.GetString("User") == "Administratie:8MmNS")
                {
                    ViewData["Message"] = null;

                    Queries Database = new Queries();

                    Database.DeleteParticipant(DeletedParticipantId);

                    return RedirectToAction("Overview", "Home", new RouteValueDictionary { { "Model", null }, { "ModelExists", 0 }, { "OnlyUnpaidParticipants", OnlyUnpaidParticipants } });
                }
            }

            return RedirectToAction("Login", "Home", new RouteValueDictionary { { "ViewMessage", "Je bent niet gemachtigd om deze actie uit te voeren." } });
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
