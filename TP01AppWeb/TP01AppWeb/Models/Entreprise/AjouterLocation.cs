using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models.Entreprise
{
    public class AjouterLocation : ReadMe
    {
        public int NoVoiture { get; set; }
        [Required(ErrorMessage = "Veuillez entrer un nombre de jours pour la location")]
        [Display(Name = "Nombre de journée de location:")]
        [Range(1, int.MaxValue, ErrorMessage = "Le nombre de jours doit être un entier positif")]
        public int? JourneeLocation { get; set; }

        [Required(ErrorMessage = "Veuillez entrer un numéro de permis")]
        [Display(Name = "Numéro de permis du client:")]
        public string NoPermisClient { get; set; }

        [Required(ErrorMessage = "Veuillez entrer un numéro de succursale de retour")]
        [Display(Name = "Code de succursale:")]
        [Range(1, int.MaxValue, ErrorMessage = "Le code doit être un entier positif")]
        public int? NoSuccursale { get; set; }

        [Display(Name = "Numéro de téléphone: \r\n Format: 0123456789")]
        [RegularExpression(@"^\d{10}$",
         ErrorMessage = "Le format du numéro de téléphone est invalide")]
        public string NoTelephone { get; set; }
        public string Nom { get; set; }

        public string Prenom { get; set; }
        public bool RequiresCreation { get; set; }
    }
}
