using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models
{
    public interface IDepot : ReadMe
    {
        IEnumerable<Utilisateur> Utilisateurs { get; }
        Utilisateur UtilisateurConn { get; }
        void AjouterUtilisateur(Utilisateur p_utilisateur);
        bool Connexion(Utilisateur p_utilisateur);
    }
}
