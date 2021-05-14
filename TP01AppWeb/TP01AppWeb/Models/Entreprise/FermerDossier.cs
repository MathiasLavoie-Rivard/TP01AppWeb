using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models.Entreprise
{
    public class FermerDossier
    {
        [Required]
        public int NoDossier {get;set;}
        [Required]
        public string NoPermis { get; set; }
        [Required]
        public int NoVoiture { get; set; }
    }
}
