using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models.Users
{
    public class UserLogin : ReadMe
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string MDP { get; set; }
    }
}
