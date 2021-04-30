using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models;
using TP01AppWeb.Models.Users;
using TP01AppWeb.Models.Entreprise;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TP01AppWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller, ReadMe
    {
        private readonly DepotEF depotef = new DepotEF();
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public HomeController(IDepot depot, UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> siginMgr)
        {
            Depot = depot;
            userManager = userMgr;
            signInManager = siginMgr;
        }

        private IDepot Depot { get; }

        [AllowAnonymous]
        public IActionResult Index()
        {
            List<string> auteurs = new List<string>();
            auteurs.Add("Xavier Hivon-Lefebvre");
            auteurs.Add("Mathias Lavoie-Rivard");
            ViewBag.Auteurs = auteurs;

            return View("Index");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Connect()
        {
            return View("Connect");
        }

        [HttpGet]
        public async Task<IActionResult> DisconnectAsync()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Connect");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Connect(UserLogin p_user)
        {
            if (ModelState.IsValid)
            {
                if (await depotef.Connexion(p_user, userManager, signInManager))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(UserLogin.Login),
                        "Mot de passe ou utilisateur invalide");
                    return View();
                }
                
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AjouterUtilisateur()
        {
            return View("AjouterUtilisateur");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AjouterUtilisateur(UserCreate p_user)
        {
            if (ModelState.IsValid)
            {
                string result = await depotef.AjouterUtilisateur(p_user, userManager);
                if (result != "SUCCESS")
                {
                    ModelState.AddModelError(nameof(UserCreate.Password), result);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
            
        }
    }
}