using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models.Entreprise;
using TP01AppWeb.Models;

namespace TP01AppWeb.Controllers
{

    [Authorize]
    public class GestionController : Controller, ReadMe
    {
        private IDepot Depot { get; }

        public GestionController(IDepot depot)
        {
            Depot = depot;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Gérant")]
        public IActionResult Succursale()
        {
            return View("Succursale");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Gérant")]
        public IActionResult Succursale(Succursale succursale)
        {
            if (ModelState.IsValid)
            {
                string result = Depot.AjouterSuccursale(succursale);
                if (result == "SUCCESS")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(succursale), result);
                    return View(succursale);
                }
            }
            return View(succursale);
        }

        [HttpGet]
        public IActionResult Voiture()
        {
            return View("Voiture");
        }

        [HttpPost]
        public IActionResult Voiture(Voiture voiture)
        {
            if (ModelState.IsValid)
            {
                string result = Depot.AjouterVoiture(voiture);
                if (result == "SUCCESS")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(voiture), result);
                    return View(voiture);
                }
            }
            return View(voiture);
        }
    }
}
