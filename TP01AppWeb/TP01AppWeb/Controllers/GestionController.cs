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
        [HttpGet]
        public IActionResult Succursales()
        {
            return View("AjouterSuccursales");
        }
        [HttpPost]
        public IActionResult Succursale(Succursale succursale)
        {
            return View("AjouterSuccursales");
        }




        [HttpGet]
        public IActionResult Voitures()
        {
            return View("AjouterVoiture");
        }

        [HttpPost]
        public IActionResult Voiture(Voiture voiture)
        {
            return View("AjouterVoiture");
        }

    }
}
