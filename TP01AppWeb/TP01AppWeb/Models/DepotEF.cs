using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models
{
    public class DepotEF : IDepot
    {
        private Utilisateur _Utilisateurconn = null;
        public Utilisateur UtilisateurConn { get { return _Utilisateurconn; } }
        private List<Utilisateur> _Utilisateurs = new List<Utilisateur>();
        public IEnumerable<Utilisateur> Utilisateurs { get { return _Utilisateurs; } }

        public DepotEF()
        {
            _Utilisateurs.Add(new Utilisateur() { Nom = "AdminI", Password = "Inimda23", TypeEmp = Utilisateur.TypeEmployer.Admin });
        }
        public void AjouterUtilisateur(Utilisateur p_utilisateur)
        {
            _Utilisateurs.Add(p_utilisateur);
        }

        public bool Connexion(Utilisateur p_utilisateur)
        {
            foreach (var u in Utilisateurs)
            {
                if (u.Nom == p_utilisateur.Nom && u.Password == p_utilisateur.Password)
                {
                    _Utilisateurconn = u;
                    return true;
                }
            }
            return false;
        }
    }
}
