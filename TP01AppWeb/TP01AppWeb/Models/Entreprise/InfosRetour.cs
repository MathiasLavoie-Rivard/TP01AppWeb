using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TP01AppWeb.Models.Entreprise
{
    public class InfosRetour : ReadMe
    {
        [Display(Name = "Date de location")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateLocation { get; set; }
        [Display(Name = "Nombre de jours de location")]
        public int? JoursLocation { get; set; }
        [Display(Name = "Code de la succursale")]
        public int? SuccursaleId { get; set; }
        [Display(Name = "Numéro de permis")]
        public string NoPermis { get; set; }
        public int NoVoiture { get; set; }
        public string Nom { get; set; }
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }
        [Display(Name = "Numéro de télépĥone")]
        public string NoTelephone { get; set; }
        public bool Accident { get; set; }
    }
}
