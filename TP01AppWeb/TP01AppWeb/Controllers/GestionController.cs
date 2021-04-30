using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models.Entreprise;

namespace TP01AppWeb.Controllers
{
    public class GestionController : Controller, ReadMe
    {
        private ContextEntreprise contextEntr;

        public GestionController(ContextEntreprise context)
        {
            contextEntr = context;
        }

        [HttpGet]
        public IActionResult Succursale()
        {
            return View("AjouterSuccursale");
        }

        [HttpPost]
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
                contextEntr.Add(voiture);
                contextEntr.SaveChanges();
            }
            catch (Exception) { }

            return View("AjouterVoiture");
        }
    }
}
