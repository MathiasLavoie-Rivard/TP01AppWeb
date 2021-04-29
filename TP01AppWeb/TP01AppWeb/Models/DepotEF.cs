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
        private UserCreate _Utilisateurconn = null;
        public UserCreate UtilisateurConn { get { return _Utilisateurconn; } }
        private List<UserCreate> _Utilisateurs = new List<UserCreate>();
        public IEnumerable<UserCreate> Utilisateurs { get { return _Utilisateurs; } }

        public DepotEF()
        {
            _Utilisateurs.Add(new UserCreate() { Nom = "AdminI", Password = "Inimda23", TypeEmp = UserCreate.TypeEmployer.Admin });
        }
        public void AjouterUtilisateur(UserCreate p_utilisateur)
        {
            _Utilisateurs.Add(p_utilisateur);
        }

        public bool Connexion(UserCreate p_utilisateur)
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
