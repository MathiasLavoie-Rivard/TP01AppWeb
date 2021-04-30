using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models.Users
{
    public class UserLogin : ReadMe
    {
        [Required(ErrorMessage = "Nom d'utilisateur manquant")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Mot de passe manquant")]
        public string MDP { get; set; }
    }
}
