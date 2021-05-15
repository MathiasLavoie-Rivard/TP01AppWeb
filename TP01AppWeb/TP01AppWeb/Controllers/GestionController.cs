using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP01AppWeb.Models.Entreprise;
using TP01AppWeb.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TP01AppWeb.Controllers
{

    [Authorize]
    public class GestionController : Controller, ReadMe
    {
        private IDepot Depot { get; }

        public GestionController(IDepot depot)
        {
            Depot = depot;
        }

        [HttpGet]
        [Authorize(Roles = "Gérant")]
        public IActionResult Succursale()
        {
            return View("Succursale");
        }

        [HttpPost]
        [Authorize(Roles = "Gérant")]
        public IActionResult Succursale(Succursale succursale)
        {
            if (ModelState.IsValid)
            {
                string result = Depot.AjouterSuccursale(succursale);
                if (result == "SUCCESS")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(succursale.NoTelephone), result);
                    return View(succursale);
                }
            }
            return View(succursale);
        }

        [HttpGet]
        [Authorize(Roles = "Commis")]
        public IActionResult Voiture()
        {
            return View("Voiture");
        }

        [HttpPost]
        [Authorize(Roles = "Commis")]
        public IActionResult Voiture(Voiture voiture)
        {
            if (ModelState.IsValid)
            {
                string result = Depot.AjouterVoiture(voiture);
                if (result == "SUCCESS")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(voiture.SuccursaleId), result);
                    return View(voiture);
                }
            }
            return View(voiture);
        }
        [HttpGet]
        [Authorize(Roles = "Commis")]
        public IActionResult RechercheLocation()
        {
            return View("RechercheLocation");
        }


        [HttpPost]
        [Authorize(Roles = "Commis")]
        public IActionResult RechercheLocation(RechercheVoiture p_recherche)
        {
            if (ModelState.IsValid)
            {
                if (Depot.VerifierSuccursale((int)p_recherche.CodeSuccursale))
                {
                    List<Voiture> Voitures = Depot.ChercherVoitures(p_recherche);

                    if (Voitures.Count != 0)
                    {
                        return View("ResultatLocation", Voitures);
                    }
                    ModelState.AddModelError(nameof(p_recherche.Model), "Aucune voiture corespondant au modèle n'est disponible");
                    return View("RechercheLocation");
                }
                else
                {
                    ModelState.AddModelError(nameof(p_recherche.CodeSuccursale), "Le code succursale est inexisant");
                    return View("RechercheLocation");
                }
            }
            else
            {
                return View("RechercheLocation");
            }

        }

        [HttpGet]
        [Authorize(Roles = "Commis")]
        public IActionResult Louer(int id)
        {

            Voiture voiture = Depot.ChercherVoitureParNo(id);
            if (voiture is null)
            {
                return View("Louer", null);
            }
            AjouterLocation location = new AjouterLocation
            {

                NoVoiture = id
            };
            return View("Louer", location);
        }

        [HttpPost]
        [Authorize(Roles = "Commis")]
        public IActionResult Louer(AjouterLocation p_Location, int id)
        {
            p_Location.NoVoiture = id;
            bool ClientExiste = Depot.VerifierClient(p_Location.NoPermisClient);
            //Vérifier si le client existe dans la base de donnée
            //Ces vérification ne peuvent pas être faite dans 
            if (!ClientExiste)
            {

                p_Location.RequiresCreation = true;

                if (!(p_Location.RequiresCreation &&
                    p_Location.Nom != null &&
                    p_Location.Prenom != null &&
                    p_Location.NoTelephone != null))
                {
                    ModelState.AddModelError(nameof(p_Location.NoPermisClient), "Le client n'existe pas dans la base de donnée");
                    //Vérifier les champs si la 
                    if (p_Location.Nom is null)
                    {
                        ModelState.AddModelError(nameof(p_Location.Nom), "Le nom est mandatoire");
                    }
                    if (p_Location.Prenom is null)
                    {
                        ModelState.AddModelError(nameof(p_Location.Prenom), "Le prénom est mandatoire");
                    }
                    if (p_Location.NoTelephone is null)
                    {
                        ModelState.AddModelError(nameof(p_Location.NoTelephone), "Numéro de téléphone est mandatoire");
                    }
                }
            }
            else
            {
                p_Location.RequiresCreation = false;

                if (!Depot.VerifierLocations(p_Location.NoPermisClient))
                {
                    ModelState.AddModelError(nameof(p_Location.NoPermisClient), "Le client à déja une location à son nom");
                }

                if (!Depot.VerifierAccident(p_Location.NoPermisClient))
                {
                    ModelState.AddModelError(nameof(p_Location.NoPermisClient), "Le client à un dossier d'accident encore actif");
                }

            }



            if (!ModelState.IsValid)
            {
                return View(p_Location);
            }
            else
            {
                if (!Depot.VerifierSuccursale((int)p_Location.NoSuccursale))
                {
                    ModelState.AddModelError(nameof(p_Location.NoSuccursale), "Le code succursale est inexisant");
                    return View(p_Location);
                }
                Client client = new Client
                {
                    NoPermis = p_Location.NoPermisClient,
                    NoTelephone = p_Location.NoTelephone,
                    Prenom = p_Location.Prenom,
                    Nom = p_Location.Nom
                };
                bool ajouter = true;
                //Ajouter le client
                if (p_Location.RequiresCreation)
                {
                    if (!Depot.AjouterClient(client))
                    {
                        ajouter = false;
                    }
                }



                if (ajouter)
                {
                    Location location = new Location
                    {
                        DateLocation = DateTime.Today,
                        JoursLocation = (int)p_Location.JoursLocation,
                        Client = client,
                        Voiture = new Voiture { NoVoiture = id },
                        SuccursaleRetourId = (int)p_Location.NoSuccursale
                    };

                    Depot.AjouterLocation(location);
                }
                else
                {
                    ModelState.AddModelError(nameof(p_Location), "La voiture n'est pas disponnible");
                    return View(p_Location);
                }



                //Ajouter la location

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Commis")]
        public IActionResult Retourner()
        {
            return View("Retourner");
        }

        [HttpPost]
        [Authorize(Roles = "Commis")]
        public IActionResult Retourner(RetournerLocation retour)
        {
            if (ModelState.IsValid)
            {
                if (Depot.VerifierSuccursale((int)retour.NoSuccursale))
                {
                    Location location = Depot.RetournerLocation(retour);
                    Client client = Depot.RetournerClient(retour);

                    if (location != null)
                    {
                        if (retour.Millage >= location.Voiture.Millage)
                        {
                            if (location.Client.Id == client.Id)
                            {
                                InfosRetour infoRetour = new InfosRetour
                                {
                                    NoTelephone = client.NoTelephone,
                                    Nom = client.Nom,
                                    Prenom = client.Prenom,
                                    NoPermis = client.NoPermis,
                                    SuccursaleId = location.SuccursaleRetourId,
                                    DateLocation = location.DateLocation,
                                    JoursLocation = location.JoursLocation
                                };
                                return View("InfosRetour", infoRetour);
                            }
                            ModelState.AddModelError(nameof(retour.NoPermisClient), "La voiture n’est pas actuellement louée au client désigné");
                            return View("Retourner");
                        }
                        ModelState.AddModelError(nameof(retour.Millage), "Le nouveau millage est inférieur au millage précédent");
                        return View("Retourner");
                    }
                    ModelState.AddModelError(nameof(retour.NoVoiture), "Aucune voiture corespondant au modèle n'est en location");
                    return View("Retourner");
                }
                else
                {
                    ModelState.AddModelError(nameof(retour.NoSuccursale), "Le code de succursale est inexisant");
                    return View("Retourner");
                }
            }
            else
            {
                return View("Retourner");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Commis")]
        public IActionResult InfosRetour(InfosRetour infos)
        {
            if (ModelState.IsValid)
            {
                RetournerLocation retour = new RetournerLocation();
                retour.NoVoiture = infos.NoVoiture;
                retour.NoPermisClient = infos.NoPermis;

                if (infos.DateLocation.AddDays((double)infos.JoursLocation).Date != DateTime.Now.Date)
                    ViewBag.Message = "La date du jour est differente de la date de retour prevue.";
                if (infos.NoSuccursale != Depot.RetournerLocation(retour).SuccursaleRetourId)
                    ViewBag.Message = "La succursale de retour n’est pas celle qui etait prevue a la location.";

                return View("InfosRetour2", infos);
            }
            else
                return View("Retourner");
        }

        [HttpPost]
        [Authorize(Roles = "Commis")]
        public IActionResult InfosRetour2(InfosRetour infos)
        {
            if (ModelState.IsValid)
            {
                RetournerLocation retour = new RetournerLocation();
                retour.NoVoiture = infos.NoVoiture;
                retour.NoPermisClient = infos.NoPermis;
                retour.Millage = infos.Millage;
                retour.NoSuccursale = infos.NoSuccursale;

                if (infos.Accident)
                {
                    DossierAccident accident = new DossierAccident();
                    accident.NoPermis = infos.NoPermis;
                    accident.LocationId = Depot.ConfirmerRetourLocation(retour).Id;
                    accident.Actif = true;

                    return View("Accident", accident);
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
                return View("InfosRetour2", infos);
        }

        [HttpPost]
        [Authorize(Roles = "Commis")]
        public IActionResult Accident(DossierAccident accident)
        {
            if (ModelState.IsValid)
            {
                Depot.AjouterAccident(accident);

                return RedirectToAction("Index", "Home");
            }
            else
                return View("Accident", accident);
        }

        [HttpGet]
        [Authorize(Roles = "Commis")]
        public IActionResult FermerDossier()
        {
            return View("FermerDossier");
        }
        [HttpPost]
        [Authorize(Roles = "Commis")]
        public IActionResult FermerDossier(FermerDossier p_Dossier)
        {
            if (ModelState.IsValid)
            {
                string error = Depot.VerifierDossierClient(p_Dossier);
                if (error == null || error == "")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(p_Dossier), error);
                    return View(p_Dossier);
                }
            }
            return View(p_Dossier);

        }
    }
}
