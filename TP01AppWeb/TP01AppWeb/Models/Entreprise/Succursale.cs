using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TP01AppWeb.Models.Entreprise
{
    public sealed class Succursale : ReadMe
    {
        [Key]
        public uint Id { get; set; }
        [Display(Name = "Code de succursale:")]
        public int Code { get; set; }
        [Display(Name = "Numéro civic:")]
        public int NoCivic { get; set; }
        [Display(Name = "Nom de rue:")]
        public string Rue { get; set; }
        [Display(Name = "Ville:")]
        public string Ville { get; set; }
        [Display(Name = "Province:")]
        public string Province { get; set; }
        [Display(Name = "Code Postal:")]
        public string CodePostal { get; set; }
        [Display(Name = "Numéro de téléphone:")]
        public string NoTelephone { get; set; }
        public List<Voiture> Voiture { get; set; }
    }
}
