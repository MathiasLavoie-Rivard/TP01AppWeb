using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models.Entreprise;

namespace TP01AppWeb.Controllers
{
    [Authorize]
    public class GestionController : Controller, ReadMe
    {
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
            try
            {
                contextEntr.Add(succursale);
                contextEntr.SaveChanges();
            }
            catch (Exception) { }

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
            try
            {
                if (contextEntr.Succursales.Any(s => s.Code == voiture.Succursale))
                {
                    if (!contextEntr.Voitures.Any(v => v.NoVoiture == voiture.NoVoiture))
                    {
                        if (!contextEntr.Voitures.Any(v => v.Model == voiture.Model && v.Groupe != voiture.Groupe))
                        {
                            contextEntr.Add(voiture);
                            contextEntr.SaveChanges();
                        }
                        else
                        {
                            ModelState.AddModelError(nameof(voiture),
                                "Il y a un autre groupe pour le même nom de modèle");
                            return View("AjouterVoiture");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(voiture),
                            "Le numéro de voiture est déjà utilisé");
                        return View("AjouterVoiture");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(voiture),
                        "Le code de succursale est invalide");
                    return View("AjouterVoiture");
                }
            }
            catch (Exception) { }

            return View("AjouterVoiture");
        }
    }
}
