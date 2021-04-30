using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models;
using TP01AppWeb.Models.Entreprise;

namespace TP01AppWeb.Controllers
{
    [Authorize]
    public class GestionController : Controller, ReadMe
    {
        private readonly DepotEF depotef = new DepotEF();
        private ContextEntreprise contextEntr;

        public GestionController(ContextEntreprise context)
        {
            contextEntr = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Gérant")]
        public IActionResult Succursale()
        {
            return View("AjouterSuccursale");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Gérant")]
        public IActionResult Succursale(Succursale succursale)
        {
            string result = depotef.AjouterSuccursale(succursale, contextEntr);
            if (result == "SUCCESS")
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(nameof(succursale),
                               result);

            return View("AjouterSuccursale");
        }

        [HttpGet]
        public IActionResult Voiture()
        {
            return View("AjouterVoiture");
        }

        [HttpPost]
        public IActionResult Voiture(Voiture voiture)
        {


            string result = depotef.AjouterVoiture(voiture, contextEntr);
            if (result == "SUCCESS")
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(nameof(voiture),
                               result);

            return View("AjouterSuccursale");
        }
    }
}
