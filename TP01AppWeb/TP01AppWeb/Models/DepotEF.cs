using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models.Users;
using TP01AppWeb.Models;
using Microsoft.AspNetCore.Identity;
using TP01AppWeb.Models.Entreprise;

namespace TP01AppWeb.Models
{
    public class DepotEF : IDepot, ReadMe
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private ContextEntreprise contextEntr;

        public DepotEF(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signinMgr, ContextEntreprise contextEnt)
        {
            contextEntr = contextEnt;
            userManager = userMgr;
            signInManager = signinMgr;
        }

        public IQueryable<Succursale> Succursales => contextEntr.Succursales;
        public IQueryable<Voiture> Voitures => contextEntr.Voitures;
        public IQueryable<IdentityUser> Users => userManager.Users;


        public async Task<bool> Connexion(UserLogin p_user)
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
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        public async Task<string> AjouterUtilisateur(UserCreate p_user)
        {
            IdentityUser usr = new IdentityUser
            {
                UserName = p_user.Nom,
            };
            IdentityResult result =
                await userManager.CreateAsync(usr, p_user.Password);
            if (result.Succeeded)
            {
                try
                {
                    await userManager.AddToRoleAsync(usr, p_user.TypeEmp.ToString());
                }
                catch (Exception)
                {
                    return "Une erreur est survenu lors de l'ajout d'un utilisateur";
                }
                return "SUCCESS";

            }
            string errors = "";
            foreach (IdentityError error in result.Errors)
            {
                errors = error.Description + "\r\n";
            }
            return errors;

        }

        public string AjouterSuccursale(Succursale p_succursale)
        {
            string result = "";

            if (!contextEntr.Succursales.Any(s => s.Code == p_succursale.Code))
            {
                if (!contextEntr.Succursales.Any(s => s.Rue == p_succursale.Rue && s.CodePostal == p_succursale.CodePostal))
                {
                    if (!contextEntr.Succursales.Any(s => s.CodePostal == p_succursale.CodePostal && s.Ville == p_succursale.Ville))
                    {
                        if (!contextEntr.Succursales.Any(s => s.CodePostal == p_succursale.CodePostal && s.Province == p_succursale.Province))
                        {
                            try
                            {
                                contextEntr.Add(p_succursale);
                                contextEntr.SaveChanges();
                                result = "SUCCESS";
                            }
                            catch (Exception)
                            {
                                result = "Une erruer est survenue lors de l'ajout à la bd\r\n";
                            }
                        }
                        else
                        {
                            result += "Il y a un autre nom de province avec le même code postal\r\n";
                        }
                    }
                    else
                    {
                        result += "Il y a un autre nom de ville avec le même code postal\r\n";
                    }
                }
                else
                {
                    result += "Il y a déjà une succursale avec le même nom de rue et code postal\r\n";
                }
            }
            else
            {
                result += "Le code de succursale est déjà utilisé\r\n";
            }

            return result;
        }

        public string AjouterVoiture(Voiture p_Voiture)
        {
            string result= "";

            Succursale succ = contextEntr.Succursales.FirstOrDefault(x => x.Code == p_Voiture.SuccursaleId);

            if (succ != null)
            {
                if (!contextEntr.Voitures.Any(v => v.NoVoiture == p_Voiture.NoVoiture))
                {
                    if (!contextEntr.Voitures.Any(v => v.Model == p_Voiture.Model && v.Groupe != p_Voiture.Groupe))
                    {
                        try
                        {
                            p_Voiture.SuccursaleId = succ.Id;
                            contextEntr.Add(p_Voiture);
                            contextEntr.SaveChanges();
                            result = "SUCCESS";
                        }
                        catch (Exception)
                        {
                            result = "Une erruer est survenue lors de l'ajout à la bd\r\n";
                        }

                    }
                    else
                    {
                        result += "Il y a un autre groupe pour le même nom de modèle";
                    }
                }
                else
                {
                        result += "Le numéro de voiture est déjà utilisé";
                }
            }
            else
            {
                result += "Le code de succursale est invalide";
            }
            return result;
        }

        public async Task DeconnexionAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
