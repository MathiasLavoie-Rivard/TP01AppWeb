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
                    return View();
                }
            }
            return View();
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
                    if (contextEntr.Succursales.Any(s => s.Code == voiture.SuccursaleId))
                    {
                        if (!contextEntr.Voitures.Any(v => v.NoVoiture == voiture.NoVoiture))
                        {
                            if (!contextEntr.Voitures.Any(v => v.Model == voiture.Model && v.Groupe != voiture.Groupe))
                            {
                                if (contextEntr.Succursales.Any(s => s.Code == voiture.SuccursaleId))
                                {
                                    contextEntr.Add(voiture);
                                    contextEntr.SaveChanges();
                                }
                                else
                                {
                                    ModelState.AddModelError(nameof(voiture),
                                        "La succursale n'existe pas.");
                                    return View(voiture);
                                }
                            }
                            else
                            {
                                ModelState.AddModelError(nameof(voiture),
                                    "Il y a un autre groupe pour le même nom de modèle");
                                return View(voiture);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(nameof(voiture),
                                "Le numéro de voiture est déjà utilisé");
                            return View(voiture);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(voiture),
                            "Le code de succursale est invalide");
                        return View(voiture);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(voiture), result);
                    return View();
                }
            }
            return View();
        }
    }
}
