using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models;

namespace TP01AppWeb.Controllers
{
    public class HomeController : Controller
    {

        private IDepot Depot { get; }

        public IActionResult Index()
        {
            return View("Index");
        }



        [HttpGet]
        public IActionResult Connect()
        {
            return View("Connect");
        }

        [HttpPost]
        public IActionResult Connect(Utilisateur p_user)
        {
            if (ModelState.IsValid)
            {
                if (Depot.Utilisateurs.Any(item => item.Nom == p_user.Nom)){

                    if (Depot.Connexion(p_user)){
                        ErrorViewModel e = new ErrorViewModel("La connection de " + p_user.Nom + " a été réussi");
                        return View("Error", e);
                    }
                    else
                    {
                        ErrorViewModel e = new ErrorViewModel("Le mot de passe n'est pas le bon");
                        return View("Error", e);
                    }

                }
                else
                {
                    ErrorViewModel e = new ErrorViewModel("Le code " + p_user.Nom + " est non existant");
                    return View("Error", e);
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult AjouterUtilisateur()
        {
            return View("AjouterUtilisateur");
        }

        [HttpPost]
        public IActionResult AjouterUtilisateur(Utilisateur p_user)
        {
            ErrorViewModel e;
            if (Depot.UtilisateurConn != null && Depot.UtilisateurConn.TypeEmp == Utilisateur.TypeEmployer.Admin)
            {
                foreach (var u in Depot.Utilisateurs)
                {
                    if (u.Nom == p_user.Nom)
                    {
                        e = new ErrorViewModel("Le code d'utilisateur existe déja");
                        return View("Error", e);
                    }
                }
                Depot.AjouterUtilisateur(p_user);
                 e = new ErrorViewModel("L'utilisateur a été ajouté avec succès");
                return View("Error", e);
            }
            else
            {
                 e = new ErrorViewModel("Vous n'êtes pas adminitrateur");
                return View("Error", e);
            }
        }
        public HomeController(IDepot depot)
        {
            Depot = depot;
        }
    }
}
