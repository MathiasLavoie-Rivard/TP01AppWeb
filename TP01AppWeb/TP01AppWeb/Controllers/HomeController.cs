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

        [HttpGet]
        public async Task<IActionResult> DisconnectAsync()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Connect");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Connect(UserLogin p_user,
                string returnUrl)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(p_user.Login);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                            await signInManager.PasswordSignInAsync(
                                user, p_user.MDP, false, false);
                    if (result.Succeeded)
                    {
                        //return RedirectToAction("Index", "Home");
                        return Redirect(returnUrl ?? "/");
                    }
                }
            }
            return View("Connect");
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
                IdentityUser usr = new IdentityUser
                {
                    UserName = p_user.Nom
                };
                IdentityResult result =
                    await userManager.CreateAsync(usr, p_user.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(usr, p_user.TypeEmp.ToString());
                    return View("Index");
                }
                else
                {
                    foreach (IdentityError erreur in result.Errors)
                    {
                        ModelState.AddModelError("", erreur.Description);
                    }
                }
            }

            return View("Index");
        }
    }
}