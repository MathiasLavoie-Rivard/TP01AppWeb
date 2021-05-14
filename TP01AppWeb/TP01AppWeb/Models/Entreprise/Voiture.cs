using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TP01AppWeb.Models.Entreprise
{
    public class Voiture : ReadMe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Numéro de la voiture:")]
        public int? NoVoiture { get; set; }
        [Required]
        [Display(Name = "Nom du modèle:")]
        public string Model { get; set; }
        [Required]
        [Range(1900, 9999)]
        [Display(Name = "Année:")]
        public int? Annee { get; set; }
        [Required]
        [Display(Name = "Groupe:")]
        public Groupes Groupe { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Millage:")]
        public int? Millage { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Code de succursale:")]
        public int? SuccursaleId { get; set; }
        public Succursale Succursale { get; set; }
        public bool Disponible { get; set; }
        public List<Location> Locations { get; set; }

        public enum Groupes
        {
            Compact,
            Sedan,
            Luxe
        }
    }
}
