using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models.Entreprise
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateLocation { get; set; }
        public int JourneeLocation { get; set; }
        public Client Client { get; set; }
        public Voiture Voiture { get; set; }
    }

}

