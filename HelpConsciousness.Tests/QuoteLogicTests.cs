using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using BLL;
using DAL;
using System.Collections.Generic;

namespace HelpConsciousness.Tests
{
    [TestClass]
    public class QuoteLogicTests
    {

        private static IHashing hashData = MockRepository.GenerateStub<IHashing>();
        private static IQuoteLogic quoteLogic = MockRepository.GenerateStub<IQuoteLogic>();
        private static IQuoteDAO quoteData = MockRepository.GenerateStub<IQuoteDAO>();
        private static ISQLDAO dataWriter = MockRepository.GenerateStub<ISQLDAO>();

        private List<QuoteDM> fakeQuotes = new List<QuoteDM>(){
            new QuoteDM{Phrase = "Cake", Author = "3.50", Category = "EA", Rating = "5"}
            ,new QuoteDM{Phrase = "Donut", Author = "1.00", Category = "EA", Rating = "24"}
            ,new QuoteDM{Phrase = "Coffee", Author = "2.00", Category = "EA", Rating = "10"}};

        private List<QuoteSM> fakeQuote = new List<QuoteSM>(){
            new QuoteSM{Phrase = "Cake", Author = "Guy", CategoryNum = 1, RatingNum = 5}
            ,new QuoteSM{Phrase = "Donut", Author = "Friend", CategoryNum = 2, RatingNum = 3}
            ,new QuoteSM{Phrase = "Coffee", Author = "Buddy", CategoryNum = 3, RatingNum = 1}};

        

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
