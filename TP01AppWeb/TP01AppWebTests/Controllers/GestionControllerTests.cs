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

namespace TP01AppWebTests.Controllers
{
    public class GestionControllerTests
    {
        private static UserManager<IdentityUser> userManager;
        private static SignInManager<IdentityUser> singInManager;
        private static ContextEntreprise context;

        private DepotEF depot = new DepotEF(userManager, singInManager, context);


        [Fact]
        public void AjouterSuccursale()
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
            depot.AjouterSuccursale(succ);
            Succursale succResult = depot.Succursales.FirstOrDefault(s => s.Code == 123456);

            //Assert
            Assert.Equal(succResult, succ);

        }
    }
}
