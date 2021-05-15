using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models.Entreprise
{
    public class Location : ReadMe
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Date de début de la location")]
        public DateTime DateLocation { get; set; }
        [Required(ErrorMessage = "Veuillez entrer une durée")]
        [Display(Name = "Durée de la location (en jours)")]
        public int JoursLocation { get; set; }
        [Display(Name = "ID du client")]
        public Client Client { get; set; }
        [Display(Name = "Numéro de voiture")]
        public Voiture Voiture { get; set; }
        [Display(Name = "Numéro de succursale de retour prévue")]
        public int SuccursaleRetourId { get; set; }
        public Succursale SuccursaleRetour { get; set; }
    }

}

