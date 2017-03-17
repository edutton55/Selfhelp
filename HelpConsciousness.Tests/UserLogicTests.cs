using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using System.Collections.Generic;
using DAL;
using Rhino.Mocks;
using ApplicationLogger;

namespace HelpConsciousness.Tests
{
    [TestClass]
    public class UserLogicTests
    {
        private static IUserLogic userLogic = MockRepository.GenerateStub<IUserLogic>();
        //private static ISQLDAO dataWriter = MockRepository.GenerateStub<ISQLDAO>();
        //private static IUserDAO userData = MockRepository.GenerateStub<IUserDAO>();
        //private static IHashing hashData = MockRepository.GenerateStub<IHashing>();
        //private static ILoggerIO logger = MockRepository.GenerateStub<ILoggerIO>();

        //private List<UserDM> fakePeople = new List<UserDM>(){
        //    new UserDM{UserName = "Ethan", Password = "Scooby5!", SecLev = "Admin"}
        //    ,new UserDM{UserName = "Will", Password = "Yellow5!", SecLev = "Power User"}
        //    ,new UserDM{UserName = "Jeff", Password = "Password5!", SecLev = "User"}};

        private List<UserSM> fakeUsers = new List<UserSM>(){
            new UserSM{UserId = 1, UserName = "Ethan", Password = "Scooby5!", SecLev = "Admin"}
            ,new UserSM{UserId = 2, UserName = "Will", Password = "Yellow5!", SecLev = "Power User"}
            ,new UserSM{UserId = 3, UserName = "Jeff", Password = "Password5!", SecLev = "User"}};


        [TestMethod]
        public void GetUsers_Test()
        {
            //Arrange
            MockRepository mocks = new MockRepository();
            IUserLogic logic = mocks.Stub<IUserLogic>();
            using (mocks.Record())
            {
                SetupResult.For(logic.GetUsers()).Return(fakeUsers);
            }
            //Act
            List<UserSM> users = logic.GetUsers();
            //Assert
            Assert.AreEqual(3, users.Count, "no employee's found");
            logic.VerifyAllExpectations();
        }
        [TestMethod]
        public void GetUserById_Test()
        {
            //Arrange
            MockRepository mocks = new MockRepository();
            IUserLogic logic = mocks.Stub<IUserLogic>();
            //UserSM user = new UserSM();
            //user.UserId = 1;
            //user.UserName = "Ethan";
            int id = 1;
            int idDos = 2;
            UserSM user = new UserSM();
            user.UserId = 1;
            user.SecLev = "Admin";
            UserSM userDos = new UserSM();
            userDos.UserId = 2;
            userDos.SecLev = "Power User";
            using (mocks.Record())
            {
                SetupResult.For(logic.GetUserById(id)).Return(user);
                SetupResult.For(logic.GetUserById(idDos)).Return(userDos);
            }
            //Act(execute) 
            UserSM testUser = logic.GetUserById(id);
            //Assert(check)
            Assert.AreEqual("Admin", testUser.SecLev);
            logic.VerifyAllExpectations();
        }
    }
}
