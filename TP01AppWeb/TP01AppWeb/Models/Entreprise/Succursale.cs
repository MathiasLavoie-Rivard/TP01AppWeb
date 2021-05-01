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
        public int Id { get; set; }
        [Required(ErrorMessage ="Veuillez entrer un numéro de succursale")]
        [Display(Name = "Code de succursale:")]
        [Range(1, int.MaxValue, ErrorMessage = "Le code doit être un entier positif")]
        public int? Code { get; set; }
        [Required(ErrorMessage = "Veuillez entrer un numéro civic")]
        [Range(1, int.MaxValue, ErrorMessage = "Le numéro civic doit être un entier positif")]
        [Display(Name = "Numéro civic:")]
        public int? NoCivic { get; set; }
        [Required]
        [Display(Name = "Nom de rue:")]
        public string Rue { get; set; }
        [Required]
        [Display(Name = "Ville:")]
        public string Ville { get; set; }
        [Required]
        [Display(Name = "Province:")]
        public string Province { get; set; }
        [Display(Name = "Code Postal:")]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z]\d[A-Za-z]\d$",
         ErrorMessage = "Le format du code postal est invalide")]
        [Required]
        public string CodePostal { get; set; }
        [Required(ErrorMessage = "Veuillez entrer un numéro de téléphone")]
        [Display(Name = "Numéro de téléphone: \r\n Format: 0123456789")]
        [RegularExpression(@"^\d{10}$",
         ErrorMessage = "Le format du numéro de téléphone est invalide")]
        public string NoTelephone { get; set; }
        public List<Voiture> Voiture { get; set; }
    }
}
