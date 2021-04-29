using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TP01AppWeb.Models.Entreprise
{
    public sealed class Succursale
    {
        [Key]
        public int Id { get; set; }
        public int NoCivic { get; set; }
        public string Rue { get; set; }
        public string Ville { get; set; }
        public string Province { get; set; }
        public string CodePostal { get; set; }
        public string PhoneNumber { get; set; }
        public List<Voiture> Voiture { get; set; }
    }
}
