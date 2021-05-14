using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TP01AppWeb.Models.Entreprise
{
    public class InfosRetour : ReadMe
    {
        public DateTime DateLocation { get; set; }
        public int? JoursLocation { get; set; }
        public int? SuccursaleId { get; set; }
        public string NoPermis { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NoTelephone { get; set; }
    }
}
