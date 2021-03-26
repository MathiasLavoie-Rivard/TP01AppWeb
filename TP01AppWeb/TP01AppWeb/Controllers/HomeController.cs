using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models;

namespace TP01AppWeb.Controllers
{
    public class HomeController : Controller
    {

        private IDepot Depot{get; }

        public IActionResult Index()
        {
            return View("Index");
        }


        
        [HttpGet]
        public IActionResult Connect()
        {
            return View("Connect");
        }

        [HttpPost]
        public IActionResult Connect(Utilisateur p_user)
        {
            if (ModelState.IsValid)
            {

                Utilisateur u = Depot.Connexion(p_user);
                if (u != null)
                {
                    return View("ConnectConfirm", u);
                }
                else
                {
                    return View("ConnectConfirm", u);
                }
            

            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult AjouterUtilisateur()
        {
            return View("AjouterUtilisateur");
        }

        [HttpPost]
        public IActionResult AjouterUtilisateur(Utilisateur p_user)
        {
            return View("AjouterUtilisateur");
        }
        public HomeController(IDepot depot)
        {
            Depot = depot;
        }
    }
}
