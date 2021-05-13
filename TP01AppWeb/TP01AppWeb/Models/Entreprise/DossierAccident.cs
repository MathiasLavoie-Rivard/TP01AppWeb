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
        public string NoPermis { get; set; }
        public Client Client { get; set; }
        public string NoVoiture { get; set; }
        public Voiture Voiture { get; set; }

    }
}
