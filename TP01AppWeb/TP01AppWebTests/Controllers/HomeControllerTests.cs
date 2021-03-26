using TP01AppWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using TP01AppWeb.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TP01AppWeb.Controllers.Tests
{
    public class HomeControllerTests
    {
        static DepotEF depot = new DepotEF();
        HomeController hc = new HomeController(depot);

        [Fact]
        public void ConnectSuccessTest()
        {
            // Arrange
            Utilisateur uti = new Utilisateur();
            uti.Nom = "AdminI";
            uti.Password = "Inimda23";

            //Act
            dynamic result = hc.Connect(uti);

            //Assert
            Assert.Equal("La connection de AdminI a réussi", result.Model.Error);
        }

        [Fact]
        public void ConnectCodeIncorrectTest()
        {
            // Arrange
            Utilisateur uti = new Utilisateur();
            uti.Nom = "Mauvais";
            uti.Password = "Inimda23";

            //Act
            dynamic result = hc.Connect(uti);

            //Assert
            Assert.Equal("Le code Mauvais est non existant", result.Model.Error);
        }

        [Fact]
        public void ConnectMdpIncorrectTest()
        {
            // Arrange
            Utilisateur uti = new Utilisateur();
            uti.Nom = "AdminI";
            uti.Password = "Mauvais";

            //Act
            dynamic result = hc.Connect(uti);

            //Assert
            Assert.Equal("Le mot de passe n'est pas le bon", result.Model.Error);
        }

        [Fact]
        public void AjouterUtilisateurSuccessTest()
        {
            // Arrange
            Mock<IDepot> mock = new Mock<IDepot>();
            Utilisateur user = new Utilisateur();
            List<Utilisateur> lstUser = new List<Utilisateur>();
            user.Nom = "AdminI";
            user.Password = "Inimda23";
            user.TypeEmp = Utilisateur.TypeEmployer.Admin;
            mock.Setup(x => x.UtilisateurConn).Returns(user);    // Mock current user
            lstUser.Add(user);
            mock.Setup(x => x.Utilisateurs).Returns(lstUser);    // Mock list of user
            HomeController mockhc = new HomeController(mock.Object);

            Utilisateur uti = new Utilisateur();
            uti.Nom = "Joe";
            uti.Password = "Joe23";

            //Act
            dynamic result = mockhc.AjouterUtilisateur(uti);

            //Assert
            Assert.Equal("L'utilisateur a été ajouté avec succès", result.Model.Error);
        }

        [Fact]
        public void AjouterUtilisateurDejaExistantTest()
        {
            // Arrange
            Mock<IDepot> mock = new Mock<IDepot>();
            Utilisateur user = new Utilisateur();
            List<Utilisateur> lstUser = new List<Utilisateur>();
            user.Nom = "AdminI";
            user.Password = "Inimda23";
            user.TypeEmp = Utilisateur.TypeEmployer.Admin;
            mock.Setup(x => x.UtilisateurConn).Returns(user);    // Mock current user
            lstUser.Add(user);
            mock.Setup(x => x.Utilisateurs).Returns(lstUser);    // Mock list of user
            HomeController mockhc = new HomeController(mock.Object);

            Utilisateur uti = new Utilisateur();
            uti.Nom = "AdminI";
            uti.Password = "Inimda23";

            //Act
            dynamic result = mockhc.AjouterUtilisateur(uti);

            //Assert
            Assert.Equal("Le code d'utilisateur existe déja", result.Model.Error);
        }
    }
}