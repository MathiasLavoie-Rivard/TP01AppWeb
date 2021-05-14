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
        [Authorize(Roles = "Gérant")]
        public IActionResult Succursale()
        {
            return View("Succursale");
        }

        [HttpPost]
        [Authorize(Roles = "Gérant")]
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
                    ModelState.AddModelError(nameof(succursale.NoTelephone), result);
                    return View(succursale);
                }
            }
            return View(succursale);
        }

        [HttpGet]
        [Authorize(Roles = "Commis")]
        public IActionResult Voiture()
        {
            return View("Voiture");
        }

        [HttpPost]
        [Authorize(Roles = "Commis")]
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
                    ModelState.AddModelError(nameof(voiture.SuccursaleId), result);
                    return View(voiture);
                }
            }
            return View(voiture);
        }
        [HttpGet]
        [Authorize(Roles = "Commis")]
        public IActionResult RechercheLocation()
        {
            return View("RechercheLocation");
        }


        [HttpPost]
        [Authorize(Roles = "Commis")]
        public IActionResult RechercheLocation(RechercheVoiture p_recherche)
        {
            if (ModelState.IsValid)
            {
                if (Depot.VerifierSuccursale((int)p_recherche.CodeSuccursale))
                {
                    List<Voiture> Voitures = Depot.ChercherVoitures(p_recherche);

                    if (Voitures.Count != 0)
                    {
                        return View("ResultatLocation", Voitures);
                    }
                    ModelState.AddModelError(nameof(p_recherche.Model), "Aucune voiture corespondant au modèle n'est disponible");
                    return View("RechercheLocation");
                }
                else
                {
                    ModelState.AddModelError(nameof(p_recherche.CodeSuccursale), "Le code succursale est inexisant");
                    return View("RechercheLocation");
                }
            }
            else
            {
                return View("RechercheLocation");
            }

        }

        [HttpGet]
        [Authorize(Roles = "Commis")]
        public IActionResult Louer(int id)
        {

            Voiture voiture = Depot.ChercherVoitureParNo(id);
            if (voiture is null)
            {
                return View("Louer",null);
            }
            AjouterLocation location = new AjouterLocation
            {

                NoVoiture = id
            };
            return View("Louer", location);
        }

        [HttpPost]
        [Authorize(Roles = "Commis")]
        public IActionResult Louer(AjouterLocation p_Location,int id)
        {
            p_Location.NoVoiture = id;
            if (!ModelState.IsValid)
            {
                return View(p_Location);
            }
            else
            {
                if (!Depot.VerifierSuccursale((int)p_Location.NoSuccursale))
                {
                    ModelState.AddModelError(nameof(p_Location.NoSuccursale), "Le code succursale est inexisant");
                    return View(p_Location);
                }

                    return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Commis")]
        public IActionResult Retourner()
        {
            return View("Retourner");
        }
    }
}
