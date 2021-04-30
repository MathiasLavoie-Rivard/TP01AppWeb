//using TP01AppWeb.Controllers;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using TP01AppWeb.Models;
//using Xunit;
//using Moq;
//using TP01AppWeb.Models.Users;
//using TP01AppWeb.Models.Entreprise;
//using Microsoft.AspNetCore.Identity;

//namespace TP01AppWeb.Controllers.Tests
//{
//    public class HomeControllerTests : ReadMe
//    {
//        private readonly UserManager<IdentityUser> userManager = new UserManager<IdentityUser>();
//        private readonly SignInManager<IdentityUser> signInManager;
//        IDepot depot;
//        HomeController hc = new HomeController(depot, userManager, signInManager);

//        [Fact]
//        public void ConnectSuccessTest()
//        {
//            // Arrange
//            UserLogin uti = new UserLogin();
//            uti.Login = "AdminI";
//            uti.MDP = "Inimda23";

//            //Act
//            dynamic result = hc.Connect(uti);

//            //Assert
//            Assert.Equal("La connection de AdminI a réussi", result.Model.Error);
//        }

//        [Fact]
//        public void ConnectCodeIncorrectTest()
//        {
//            // Arrange
//            UserLogin uti = new UserLogin();
//            uti.Login = "Mauvais";
//            uti.MDP = "Inimda23";

//            //Act
//            dynamic result = hc.Connect(uti);

//            //Assert
//            Assert.Equal("Le code Mauvais est non existant", result.Model.Error);
//        }

//        [Fact]
//        public void ConnectMdpIncorrectTest()
//        {
//            // Arrange
//            UserLogin uti = new UserLogin();
//            uti.Login = "AdminI";
//            uti.MDP = "Mauvais";

//            //Act
//            dynamic result = hc.Connect(uti);

//            //Assert
//            Assert.Equal("Le mot de passe n'est pas le bon", result.Model.Error);
//        }

//        [Fact]
//        public void AjouterUtilisateurSuccessTest()
//        {
//            // Arrange
//            Mock<IDepot> mock = new Mock<IDepot>();
//            Utilisateur user = new Utilisateur();
//            List<Utilisateur> lstUser = new List<Utilisateur>();
//            user.Nom = "AdminI";
//            user.Password = "Inimda23";
//            user.TypeEmp = Utilisateur.TypeEmployer.Admin;
//            mock.Setup(x => x.UtilisateurConn).Returns(user);    // Mock current user
//            lstUser.Add(user);
//            mock.Setup(x => x.Utilisateurs).Returns(lstUser);    // Mock list of user
//            HomeController mockhc = new HomeController(mock.Object);

//            Utilisateur uti = new Utilisateur();
//            uti.Nom = "Joe";
//            uti.Password = "Joe23";

//            //Act
//            dynamic result = mockhc.AjouterUtilisateur(uti);

//            //Assert
//            Assert.Equal("L'utilisateur a été ajouté avec succès", result.Model.Error);
//        }

//        [Fact]
//        public void AjouterUtilisateurDejaExistantTest()
//        {
//            // Arrange
//            Mock<IDepot> mock = new Mock<IDepot>();
//            UserCreate user = new UserCreate();
//            List<UserCreate> lstUser = new List<UserCreate>();
//            user.Nom = "AdminI";
//            user.Password = "Inimda23";
//            user.TypeEmp = UserCreate.TypeEmployer.Admin;
//            mock.Setup(x => x.UtilisateurConn).Returns(user);    // Mock current user
//            lstUser.Add(user);
//            mock.Setup(x => x.Utilisateurs).Returns(lstUser);    // Mock list of user
//            HomeController mockhc = new HomeController(mock.Object);

//            UserCreate uti = new UserCreate();
//            uti.Nom = "AdminI";
//            uti.Password = "Inimda23";

//            //Act
//            dynamic result = mockhc.AjouterUtilisateur(uti);

//            //Assert
//            Assert.Equal("Le code d'utilisateur existe déja", result.Model.Error);
//        }
//    }
//}