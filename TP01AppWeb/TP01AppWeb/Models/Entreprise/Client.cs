using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models.Entreprise
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Numéro de permis")]
        public string NoPermis { get; set; }
        [Required(ErrorMessage = "Veuillez entrer un numéro de téléphone")]
        [Display(Name = "Numéro de téléphone: \r\n Format: 0123456789")]
        [RegularExpression(@"^\d{10}$",
         ErrorMessage = "Le format du numéro de téléphone est invalide")]
        public string NoTelephone { get; set; }
        [Required(ErrorMessage = "Veuillez entrer un nom")]
        public string Nom { get; set; }
        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Veuillez entrer un prénom")]
        public string Prenom { get; set; }
    }
}
