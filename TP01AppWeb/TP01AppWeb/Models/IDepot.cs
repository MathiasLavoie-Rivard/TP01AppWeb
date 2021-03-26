using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models
{
    public interface IDepot
    {
        IEnumerable<Utilisateur> Utilisateurs { get; }
        Utilisateur UtilisateurConn { get; }
        void AjouterUtilisateur(Utilisateur p_utilisateur);
        Utilisateur Connexion(Utilisateur p_utilisateur);
    }
}
