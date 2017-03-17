using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using DAL;
using System.Collections.Generic;
using Rhino.Mocks;

namespace HelpConsciousness.Tests
{
    [TestClass]
    public class RatingLogicTests
    {
        private static IRatingLogic ratingLogic = MockRepository.GenerateStub<IRatingLogic>();
        private static IRatingDAO itemData = MockRepository.GenerateStub<IRatingDAO>();

        private List<RatingDM> fakeRatings = new List<RatingDM>(){
            new RatingDM{Rating = "Far Out"}
            , new RatingDM{Rating = "Noice"}
            , new RatingDM{Rating = "MindBlowing"}};

        private List<RatingSM> fakeRating = new List<RatingSM>(){
            new RatingSM{Rating = "Far Out"}
            , new RatingSM{Rating = "Noice"}
            , new RatingSM{Rating = "MindBlowing"}};

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
