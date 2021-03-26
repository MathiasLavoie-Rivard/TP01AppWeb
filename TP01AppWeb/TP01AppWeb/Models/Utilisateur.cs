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
        [RegularExpression("^[a-zA-Z0-9]{6}$", ErrorMessage = "Le code doit être une chaîne de 6 caractères alphanumériques")]
        [Required(ErrorMessage = "Veuillez entrer un nom")]
        public string Nom { get; set; }
        [RegularExpression("^(?=.*\\d).{8,}$", ErrorMessage = "Le mot de passe doit avoir au moins un chiffre et avoir au moins 8 caractères")]
        [Required(ErrorMessage = "Veuillez entrer un mot de passe")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Veuillez choisir le type")]
        public TypeEmployer TypeEmp { get; set; }
    }
}
