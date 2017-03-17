using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using System.Collections.Generic;
using BLL;
using Rhino.Mocks;

namespace HelpConsciousness.Tests
{
    [TestClass]
    public class CategoryLogicTests
    {
        private static ICategoryDAO categoryData = MockRepository.GenerateStub<ICategoryDAO>();
        private static ICategoryLogic categoryLogic = MockRepository.GenerateStub<ICategoryLogic>();

        private List<CategoryDM> fakeCategory = new List<CategoryDM>(){
            new CategoryDM{Category = "Happiness"}
            , new CategoryDM{Category = "Love"}
            , new CategoryDM{Category = "Life"}};

        private List<CategorySM> fakeCategories = new List<CategorySM>(){
            new CategorySM{Category = "Happiness"}
            , new CategorySM{Category = "Love"}
            , new CategorySM{Category = "Life"}};

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
