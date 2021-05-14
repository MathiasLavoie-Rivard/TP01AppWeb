using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models.Users;
using TP01AppWeb.Models;
using Microsoft.AspNetCore.Identity;
using TP01AppWeb.Models.Entreprise;

namespace TP01AppWeb.Models
{
    public class DepotEF : IDepot, ReadMe
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private ContextEntreprise contextEntr;

        public DepotEF(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signinMgr, ContextEntreprise contextEnt)
        {
            contextEntr = contextEnt;
            userManager = userMgr;
            signInManager = signinMgr;
        }

        public IQueryable<Succursale> Succursales => contextEntr.Succursales;
        public IQueryable<Voiture> Voitures => contextEntr.Voitures;
        public IQueryable<IdentityUser> Users => userManager.Users;


        public async Task<bool> Connexion(UserLogin p_user)
        {
            IdentityUser user = await userManager.FindByNameAsync(p_user.Login);
            if (user != null)
            {
                await signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result =
                        await signInManager.PasswordSignInAsync(
                            user, p_user.MDP, false, false);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        public async Task<string> AjouterUtilisateur(UserCreate p_user)
        {
            IdentityUser usr = new IdentityUser
            {
                UserName = p_user.Nom,
            };
            IdentityResult result =
                await userManager.CreateAsync(usr, p_user.Password);
            if (result.Succeeded)
            {
                try
                {
                    await userManager.AddToRoleAsync(usr, p_user.TypeEmp.ToString());
                }
                catch (Exception)
                {
                    return "Une erreur est survenu lors de l'ajout d'un utilisateur";
                }
                return "SUCCESS";

            }
            string errors = "";
            foreach (IdentityError error in result.Errors)
            {
                errors = error.Description + "\r\n";
            }
            return errors;

        }

        public string AjouterSuccursale(Succursale p_succursale)
        {
            string result = "";

            if (!contextEntr.Succursales.Any(s => s.Code == p_succursale.Code))
            {
                if (!contextEntr.Succursales.Any(s => s.Rue == p_succursale.Rue && s.CodePostal == p_succursale.CodePostal))
                {
                    if (!contextEntr.Succursales.Any(s => s.CodePostal == p_succursale.CodePostal && s.Ville == p_succursale.Ville))
                    {
                        if (!contextEntr.Succursales.Any(s => s.CodePostal == p_succursale.CodePostal && s.Province == p_succursale.Province))
                        {
                            try
                            {
                                contextEntr.Add(p_succursale);
                                contextEntr.SaveChanges();
                                result = "SUCCESS";
                            }
                            catch (Exception)
                            {
                                result = "Une erruer est survenue lors de l'ajout à la bd\r\n";
                            }
                        }
                        else
                        {
                            result += "Il y a un autre nom de province avec le même code postal\r\n";
                        }
                    }
                    else
                    {
                        result += "Il y a un autre nom de ville avec le même code postal\r\n";
                    }
                }
                else
                {
                    result += "Il y a déjà une succursale avec le même nom de rue et code postal\r\n";
                }
            }
            else
            {
                result += "Le code de succursale est déjà utilisé\r\n";
            }

            return result;
        }

        public string AjouterVoiture(Voiture p_Voiture)
        {
            string result = "";

            Succursale succ = contextEntr.Succursales.FirstOrDefault(x => x.Code == p_Voiture.SuccursaleId);

            if (succ != null)
            {
                if (!contextEntr.Voitures.Any(v => v.NoVoiture == p_Voiture.NoVoiture))
                {
                    if (!contextEntr.Voitures.Any(v => v.Model == p_Voiture.Model && v.Groupe != p_Voiture.Groupe))
                    {
                        try
                        {
                            p_Voiture.SuccursaleId = succ.Id;
                            p_Voiture.Succursale = succ;
                            p_Voiture.Disponible = true;
                            contextEntr.Add(p_Voiture);
                            contextEntr.SaveChanges();
                            result = "SUCCESS";
                        }
                        catch (Exception)
                        {
                            result = "Une erruer est survenue lors de l'ajout à la bd\r\n";
                        }

                    }
                    else
                    {
                        result += "Il y a un autre groupe pour le même nom de modèle";
                    }
                }
                else
                {
                    result += "Le numéro de voiture est déjà utilisé";
                }
            }
            else
            {
                result += "Le code de succursale est invalide";
            }
            return result;
        }

        public async Task DeconnexionAsync()
        {
            await signInManager.SignOutAsync();
        }

        public List<Voiture> ChercherVoitures(RechercheVoiture p_recherche)
        {
            //Aller chercher la liste des voitures
            List<Voiture> Voitures = contextEntr.Voitures.Where(x => x.Succursale.Code == p_recherche.CodeSuccursale && x.Model == p_recherche.Model && x.Disponible == true).ToList();
            //Si aucunes voitures ne sont disponible aller chercher les autres voitures du meme groupe et de meme succursale.
            if (Voitures.Count == 0)
            {
                Voiture voiture = contextEntr.Voitures.Where(x => x.Model.ToLower() == p_recherche.Model.ToLower()).FirstOrDefault();
                if (voiture != null)
                {
                    Voiture.Groupes GroupeVoiture = voiture.Groupe;
                    Voitures = contextEntr.Voitures.Where(x => x.Succursale.Code == p_recherche.CodeSuccursale && x.Groupe == GroupeVoiture && x.Disponible == true).ToList();
                }
            }
            return Voitures;
        }
        public Voiture ChercherVoitureParNo(int p_no)
        {
            Voiture voiture = contextEntr.Voitures.Where(x => x.NoVoiture == p_no).FirstOrDefault();
            if (voiture is null)
            {
                return null;
            }
            else if (voiture.Disponible == false)
            {
                return null;
            }
            return voiture;
        }
        public Voiture ChercherVoitureParNoRetour(int p_no)
        {
            Voiture voiture = contextEntr.Voitures.Where(x => x.NoVoiture == p_no).FirstOrDefault();
            if (voiture is null)
            {
                return null;
            }
            else if (voiture.Disponible == true)
            {
                return null;
            }
            return voiture;
        }

        public bool VerifierSuccursale(int p_CodeSuccursale)
        {
            if (contextEntr.Succursales.Where(x => x.Code == p_CodeSuccursale).FirstOrDefault() is null)
            {
                return false;
            }
            return true;
        }

        public bool VerifierClient(string p_NoPermis)
        {

            if (contextEntr.Clients.Where(x => x.NoPermis == p_NoPermis).FirstOrDefault() is null)
            {
                return false;
            }

            return true;
        }

        public bool AjouterClient(Client p_client)
        {
            contextEntr.Clients.Add(p_client);
            try
            {
                contextEntr.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
                return false;
            }
        }

        public bool AjouterLocation(Location p_Location)
        {
            Voiture voiture = contextEntr.Voitures.Where(x => x.NoVoiture == p_Location.Voiture.NoVoiture).FirstOrDefault();
            Succursale succursale = contextEntr.Succursales.Where(x => x.Code == p_Location.SuccursaleRetourId).FirstOrDefault();
            Client client = contextEntr.Clients.Where(x => x.NoPermis == p_Location.Client.NoPermis).FirstOrDefault();

            p_Location.Client = client;
            p_Location.SuccursaleRetour = succursale;
            p_Location.Voiture = voiture;
            voiture.Disponible = false;

            contextEntr.Update(voiture);
            contextEntr.Locations.Add(p_Location);

            try
            {
                contextEntr.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
                return false;
            }
        }

        public Location RetournerLocation(RetournerLocation retour)
        {
            Location location = contextEntr.Locations.FirstOrDefault(l => l.Voiture.NoVoiture == retour.NoVoiture && l.Client.NoPermis == retour.NoPermisClient);

            return location;
        }

        public Client RetournerClient(RetournerLocation retour)
        {
            Client client = contextEntr.Clients.FirstOrDefault(c => c.NoPermis == retour.NoPermisClient);

            return client;
        }
    }
}
