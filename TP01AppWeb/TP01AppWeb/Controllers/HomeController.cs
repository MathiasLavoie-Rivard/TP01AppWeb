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

namespace TP01AppWeb.Controllers
{


    [Authorize]
    public class HomeController : Controller, ReadMe
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public HomeController(IDepot depot, UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> siginMgr )
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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Connect(Utilisateur p_user)
        {
            //if (ModelState.IsValid)
            //{
            //    IdentityUser user = await userManager.FindByNameAsync
            //}
            return View("Index");
        }

        [HttpGet]
        public IActionResult AjouterUtilisateur()
        {
            return View("AjouterUtilisateur");
        }

        [HttpPost]
        public IActionResult AjouterUtilisateur(Utilisateur p_user)
        {
            //ErrorViewModel e;
            //Utilisateur currentUser = contextUser.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            //if (User.Identity.IsAuthenticated && currentUser.TypeEmp == Utilisateur.TypeEmployer.Admin)
            //{
            //    foreach (var u in Depot.Utilisateurs)
            //    {
            //        if (u.Nom == p_user.Nom)
            //        {
            //            e = new ErrorViewModel("Le code d'utilisateur existe déja");
            //            return View("Error", e);
            //        }
            //    }
            //    Depot.AjouterUtilisateur(p_user);
            //    e = new ErrorViewModel("L'utilisateur a été ajouté avec succès");
            //    return View("Error", e);
            //}
            //else
            //{
            //    e = new ErrorViewModel("Vous n'êtes pas administrateur");
                return View("Error");
            //}
        }
    }
}