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

        [Fact]
        public void AjouterSuccursale()
        {
            //Arrange
            Mock<IDepot> mock = new Mock<IDepot>();
            GestionController controller = new GestionController(mock.Object);

            Succursale succ = new Succursale();
            succ.Code = 123456;
            succ.CodePostal = "J5C9T6";
            succ.NoCivic = 6548;
            succ.NoTelephone = "0123456789";
            succ.Province = "QC";
            succ.Rue = "Rue de la patate";
            succ.Ville = "Saint-Hyacinthe";

            
            //Act
            controller.Succursale(succ);

            mock.Setup(x => x.Succursales).

        }
    }
}
