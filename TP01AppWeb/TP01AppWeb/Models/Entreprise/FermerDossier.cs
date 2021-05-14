using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models.Entreprise
{
    public class FermerDossier
    {
        [Required(ErrorMessage = "Le no de dossier est mandatoire")]
        public int? NoDossier {get;set;}
        [Required(ErrorMessage = "Le no de permis est mandatoire")]
        public string NoPermis { get; set; }
        [Required(ErrorMessage = "Le no de voiture est mandatoire")]
        public int? NoVoiture { get; set; }
    }
}
