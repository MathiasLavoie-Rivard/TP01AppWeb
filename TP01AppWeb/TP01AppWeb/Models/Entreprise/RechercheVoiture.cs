using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static TP01AppWeb.Models.Entreprise.Voiture;

namespace TP01AppWeb.Models.Entreprise
{
    public class RechercheVoiture
    {
        [Required]
        [Display(Name = "Nom du modèle:")]
        public string Model { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Code de succursale:")]
        public int? SuccursaleId { get; set; }
    }
}
