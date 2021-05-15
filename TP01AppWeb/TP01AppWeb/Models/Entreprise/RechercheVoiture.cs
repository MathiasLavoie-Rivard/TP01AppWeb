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
        [Required(ErrorMessage = "Veuillez entrer un nom de modèle")]
        [Display(Name = "Nom du modèle:")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Veuillez entrer un code de succursale")]
        [Range(1, int.MaxValue)]
        [Display(Name = "Code de succursale:")]
        public int? CodeSuccursale { get; set; }
    }
}
