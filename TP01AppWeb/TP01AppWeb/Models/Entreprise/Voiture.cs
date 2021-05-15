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
        [Required(ErrorMessage = "Veuillez entrer un numéro de voiture")]
        [Range(1, int.MaxValue, ErrorMessage = "Le numéro de voiture doit être positif")]
        [Display(Name = "Numéro de la voiture:")]
        public int? NoVoiture { get; set; }
        [Required(ErrorMessage = "Veuillez entrer un modèle")]
        [Display(Name = "Nom du modèle:")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Veuillez entrer l'année")]
        [Range(1900, 9999, ErrorMessage = "L'année doit être située entre 1900 et 9999")]
        [Display(Name = "Année:")]
        public int? Annee { get; set; }
        [Required(ErrorMessage = "Veuillez entrer le groupe")]
        [Display(Name = "Groupe:")]
        public Groupes Groupe { get; set; }
        [Required(ErrorMessage = "Veuillez entrer le millage")]
        [Range(1, int.MaxValue, ErrorMessage = "Le millage doit être un entier positif")]
        [Display(Name = "Millage:")]
        public int? Millage { get; set; }
        [Required(ErrorMessage = "Veuillez entrer le code de succursale")]
        [Range(1, int.MaxValue, ErrorMessage = "Le code de succursale doit être un entier positif")]
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
