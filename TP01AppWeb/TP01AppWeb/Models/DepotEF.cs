using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models.Users;
using TP01AppWeb.Models;
using Microsoft.AspNetCore.Identity;

namespace TP01AppWeb.Models
{
    public class DepotEF : IDepot, ReadMe
    {
        public DepotEF()
        {

        }

        public async Task<bool> Connexion(UserLogin p_user, UserManager<IdentityUser> p_usrManager,
            SignInManager<IdentityUser> p_SignManager)
        {
            IdentityUser user = await p_usrManager.FindByNameAsync(p_user.Login);
            if (user != null)
            {
                await p_SignManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result =
                        await p_SignManager.PasswordSignInAsync(
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

        public async Task<string> AjouterUtilisateur(UserCreate p_user, UserManager<IdentityUser> p_usrManager)
        {
            IdentityUser usr = new IdentityUser
            {
                UserName = p_user.Nom,
            };
            IdentityResult result =
                await p_usrManager.CreateAsync(usr, p_user.Password);
            if (result.Succeeded)
            {
                try
                {
                await p_usrManager.AddToRoleAsync(usr, p_user.TypeEmp.ToString());
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
    }
}
