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
        DepotEF depotef = new DepotEF();
        private ContextEntreprise contextEntr;

        public GestionController(ContextEntreprise context)
        {
            contextEntr = context;
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
                string result = depotef.AjouterSuccursale(succursale, contextEntr);
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
                string result = depotef.AjouterVoiture(voiture, contextEntr);
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
