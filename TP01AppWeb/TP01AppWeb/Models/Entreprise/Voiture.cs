using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TP01AppWeb.Models
{
    public sealed class Voiture
    {
        [Key]
        public int NoVoiture { get; set; }
        public string Model { get; set; }
        public int Annee { get; set; }
        public Groupes Groupe { get; set; }
        public int Millage { get; set; }

        public enum Groupes
        {
            Compact,
            Sedan,
            Luxe
        }
    }
}
