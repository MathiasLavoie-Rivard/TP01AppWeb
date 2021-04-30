using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TP01AppWeb.Models.Entreprise
{
    public sealed class Voiture : ReadMe
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Range(1, int.MaxValue)]
        public int NoVoiture { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [Range(1900, 9999)]
        public int Annee { get; set; }
        [Required]
        public Groupes Groupe { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Millage { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Succursale { get; set; }

        public enum Groupes
        {
            Compact,
            Sedan,
            Luxe
        }
    }
}
