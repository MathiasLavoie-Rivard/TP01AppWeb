using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models.Users;

namespace TP01AppWeb.Models
{
    public interface IDepot : ReadMe
    {
        IEnumerable<UserCreate> Utilisateurs { get; }
        UserCreate UtilisateurConn { get; }
        void AjouterUtilisateur(UserCreate p_utilisateur);
        bool Connexion(UserCreate p_utilisateur);
    }
}
