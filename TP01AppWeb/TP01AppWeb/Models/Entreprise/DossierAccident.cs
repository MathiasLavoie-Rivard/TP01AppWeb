using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models.Entreprise
{
    public class DossierAccident
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Numéro de permis")]
        public string NoPermis { get; set; }
        [Display(Name = "ID de location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public bool Actif { get; set; }
        [Display(Name = "Rapport d'accident")]
        public string Description { get; set; }
    }
}
