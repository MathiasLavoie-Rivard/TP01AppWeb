using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using TP01AppWeb.Controllers;
using System;
using System.Text;
using TP01AppWeb.Models;
using TP01AppWeb.Models.Users;
using TP01AppWeb.Models.Entreprise;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TP01AppWebTests.Controllers
{
    public class DepotEFTests
    {
        private static DbContextOptions<TP01AppWeb.Models.Entreprise.ContextEntreprise> contextOptions = new DbContextOptionsBuilder<ContextEntreprise>().UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Entreprise;MultipleActiveResultSets=True").Options;
        private static UserManager<IdentityUser> userManager;
        private static SignInManager<IdentityUser> singInManager;
        private static ContextEntreprise context = new ContextEntreprise(contextOptions);

        private DepotEF depot = new DepotEF(userManager, singInManager, context);


        [Fact]
        public void AjouterSuccursaleTest()
        {
            //Arrange
            Succursale succ = new Succursale();
            succ.Code = 123654;
            succ.CodePostal = "J5C9T4";
            succ.NoCivic = 6547;
            succ.NoTelephone = "0123456457";
            succ.Province = "QC";
            succ.Rue = "Rue de la patate pillée";
            succ.Ville = "Saint-Jean";
            
            //Act
            string result = depot.AjouterSuccursale(succ);
            Succursale succResult = context.Succursales.FirstOrDefault(s => s.Code == 123654);

            //Assert
            Assert.Equal(succResult.Code, succ.Code);
            Assert.Equal(succResult.CodePostal, succ.CodePostal);
            Assert.Equal(succResult.NoCivic, succ.NoCivic);
            Assert.Equal(succResult.NoTelephone, succ.NoTelephone);
            Assert.Equal(succResult.Province, succ.Province);
            Assert.Equal(succResult.Rue, succ.Rue);
            Assert.Equal(succResult.Ville, succ.Ville);
            Assert.Equal("SUCCESS", result);

            //Clear
            context.Succursales.Remove(succResult);
            context.SaveChanges();
        }

        [Fact]
        public void AjouterVoitureTest()
        {
            //Arrange
            Voiture voit = new Voiture();
            voit.Annee = 2006;
            voit.Groupe = Voiture.Groupes.Luxe;
            voit.Millage = 45;
            voit.Model = "Saab Aero-X";
            voit.NoVoiture = 15;
            voit.SuccursaleId = 1;
            Succursale succ = new Succursale();
            succ.Code = 1;
            succ.CodePostal = "J5C9T6";
            succ.NoCivic = 6548;
            succ.NoTelephone = "0123456789";
            succ.Province = "QC";
            succ.Rue = "Rue de la patate";
            succ.Ville = "Saint-Hyacinthe";
            context.Succursales.Add(succ);
            context.SaveChanges();

            //Act
            string result = depot.AjouterVoiture(voit);
            Voiture voitResult = context.Voitures.FirstOrDefault(v => v.Model == "Saab Aero-X");

            //Assert
            Assert.Equal(voitResult.Annee, voit.Annee);
            Assert.Equal(voitResult.Groupe, voit.Groupe);
            Assert.Equal(voitResult.Millage, voit.Millage);
            Assert.Equal(voitResult.Model, voit.Model);
            Assert.Equal(voitResult.NoVoiture, voit.NoVoiture);
            Assert.Equal(voitResult.SuccursaleId, voit.SuccursaleId);
            Assert.Equal("SUCCESS", result);

            //Clear
            context.Voitures.Remove(voitResult);
            context.Succursales.Remove(succ);
            context.SaveChanges();
        }

        [Fact]
        public void ConfirmerRetourLocationTest()
        {
            //Arrange
            Succursale succ = new Succursale();
            succ.Code = 1;
            succ.CodePostal = "J5C9T6";
            succ.NoCivic = 6548;
            succ.NoTelephone = "0123456789";
            succ.Province = "QC";
            succ.Rue = "Rue de la patate";
            succ.Ville = "Saint-Hyacinthe";
            context.Succursales.Add(succ);
            context.SaveChanges();
            Voiture voit = new Voiture();
            voit.Annee = 2011;
            voit.Groupe = Voiture.Groupes.Sedan;
            voit.Millage = 95;
            voit.Model = "Saab 9-3";
            voit.NoVoiture = 18;
            voit.Succursale = succ;
            context.Voitures.Add(voit);
            Location location = new Location();
            location.Voiture = voit;
            location.Voiture.Id = voit.Id;
            Client client = new Client();
            client.Nom = "Gérard";
            client.Prenom = "Bob";
            client.NoTelephone = "14502154484";
            client.NoPermis = "125";
            location.Client = client;
            location.Voiture.Disponible = false;
            location.SuccursaleRetour = succ;
            context.Locations.Add(location);
            RetournerLocation retour = new RetournerLocation();
            retour.NoVoiture = (int)voit.NoVoiture;
            retour.Millage = voit.Millage + 5;
            retour.NoSuccursale = succ.Code;
            retour.NoPermisClient = location.Client.NoPermis;
            context.SaveChanges();

            //Act
            depot.ConfirmerRetourLocation(retour);
            Voiture voitResult = context.Voitures.Where(v => v.NoVoiture == voit.NoVoiture).ToList().Last();

            //Assert
            Assert.Equal(voitResult.NoVoiture, voit.NoVoiture);
            Assert.Equal(voitResult.Model, voit.Model);
            Assert.Equal(voitResult.Annee, voit.Annee);
            Assert.Equal(voitResult.Millage, voit.Millage);
            Assert.Equal(voitResult.Groupe, voit.Groupe);
            Assert.Equal(voitResult.SuccursaleId, voit.SuccursaleId);
            Assert.Equal(voitResult.Disponible, voit.Disponible);
            Assert.Equal(voitResult.SuccursaleId, voit.SuccursaleId);
        }
    }
}
