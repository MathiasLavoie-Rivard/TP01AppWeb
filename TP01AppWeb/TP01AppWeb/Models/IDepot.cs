using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models.Users;

namespace TP01AppWeb.Models
{
    public interface IDepot : ReadMe
    {
        Task<string> AjouterUtilisateur(UserCreate p_user, UserManager<IdentityUser> p_usrManager);
        Task<bool> Connexion(UserLogin p_user, UserManager<IdentityUser> p_usrManager,
            SignInManager<IdentityUser> p_SignManager);
    }
}
