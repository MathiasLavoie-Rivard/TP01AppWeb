using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models
{
    public class Utilisateur
    {
        public enum TypeEmployer
        {
            Admin,
            Gérant,
            Commis
        }
        [Required(ErrorMessage = "Veuillez entrer un nom")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Veuillez entrer un mot de passe")]
        public string Password { get; set; }
        public TypeEmployer TypeEmp { get; set; }
    }
}
