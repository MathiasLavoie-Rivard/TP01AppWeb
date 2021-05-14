using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models.Entreprise
{
    public class RetournerLocation : ReadMe
    {
        [Display(Name = "Numéro de voiture: ")]
        public int NoVoiture { get; set; }
        [Required(ErrorMessage = "Veuillez entrer le nouveau millage")]
        [Range(1, int.MaxValue)]
        [Display(Name = "Millage:")]
        public int? Millage { get; set; }

        [Required(ErrorMessage = "Veuillez entrer un numéro de permis")]
        [Display(Name = "Numéro de permis du client:")]
        public string NoPermisClient { get; set; }

        [Required(ErrorMessage = "Veuillez entrer un numéro de succursale de retour")]
        [Display(Name = "Code de succursale:")]
        [Range(1, int.MaxValue, ErrorMessage = "Le code doit être un entier positif")]
        public int? NoSuccursale { get; set; }
    }
}
