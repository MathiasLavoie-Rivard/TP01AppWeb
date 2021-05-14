using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models.Users;
using TP01AppWeb.Models.Entreprise;

namespace TP01AppWeb.Models
{
    public interface IDepot : ReadMe
    {
        IQueryable<Succursale> Succursales { get; }
        IQueryable<Voiture> Voitures { get; }
        IQueryable<IdentityUser> Users { get; }
        Task<string> AjouterUtilisateur(UserCreate p_user);
        Task<bool> Connexion(UserLogin p_user);
        Task DeconnexionAsync();
        string AjouterSuccursale(Succursale p_succursale);
        string AjouterVoiture(Voiture p_Voiture);
        List<Voiture> ChercherVoitures(RechercheVoiture p_recherche);
        Voiture ChercherVoitureParNo(int p_no);
        Voiture ChercherVoitureParNoRetour(int p_no);
        bool VerifierSuccursale(int p_NoSuccursale);
        bool VerifierClient(string p_NoPermis);
        bool AjouterClient(Client p_client);
        bool AjouterLocation(Location p_Location);
        Location RetournerLocation(RetournerLocation retour);
        Client RetournerClient(RetournerLocation retour);
        bool VerifierLocations(string NoPermis);
        bool VerifierAccident(string NoPermis);
        string VerifierDossierClient(FermerDossier dossier);

    }
}
