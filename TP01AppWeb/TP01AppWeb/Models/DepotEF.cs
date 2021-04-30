using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models.Users;
using TP01AppWeb.Models;

namespace TP01AppWeb.Models
{
    public class DepotEF : IDepot, ReadMe
    {
        public DepotEF()
        {
            
        }
        public void AjouterUtilisateur(UserCreate p_utilisateur)
        {
            //TODO
        }

        public bool Connexion(UserCreate p_utilisateur)
        {
            //TODO
            return false;
        }
    }
}
