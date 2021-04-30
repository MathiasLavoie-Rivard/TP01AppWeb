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
            succ.Code = 123456;
            succ.CodePostal = "J5C9T6";
            succ.NoCivic = 6548;
            succ.NoTelephone = "0123456789";
            succ.Province = "QC";
            succ.Rue = "Rue de la patate";
            succ.Ville = "Saint-Hyacinthe";
            
            //Act
            string result = depot.AjouterSuccursale(succ);
            Succursale succResult = context.Succursales.FirstOrDefault(s => s.Code == 123456);

            //Assert
            Assert.Equal(succResult, succ);
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

            //Act
            string result = depot.AjouterVoiture(voit);
            Voiture voitResult = context.Voitures.FirstOrDefault(v => v.Model == "Saab Aero-X");

            //Assert
            Assert.Equal(voitResult, voit);
            Assert.Equal("SUCCESS", result);

            //Clear
            context.Voitures.Remove(voitResult);
            context.SaveChanges();
        }
    }
}
