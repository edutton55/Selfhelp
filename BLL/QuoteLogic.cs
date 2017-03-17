using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ApplicationLogger;

namespace BLL
{
    public class QuoteLogic:IQuoteLogic
    {
        private IQuoteDAO quoteData;
        private static ILoggerIO logs;

        public QuoteLogic(IQuoteDAO quoteDAO,ISQLDAO dao, ILoggerIO log)
        {
            logs = log;
            quoteData = quoteDAO;   //injecting dependency
            quoteData.GetDataWriter(dao);//dependency injector through a infrastructure
        }
        public List<QuoteSM> GetQuotes()
        {
            return Map(quoteData.GetQuotes());
        }
        private QuoteSM Map(QuoteDM quote)
        {
            QuoteSM phrase = new QuoteSM();
            phrase.QuoteId = Convert.ToInt32(quote.QuoteId);
            phrase.Phrase = quote.Phrase;
            phrase.Author = quote.Author;
            phrase.CategoryNum = Convert.ToInt32(quote.Category);
            phrase.RatingNum = Convert.ToInt32(quote.Rating);
            phrase.NumRatings = Convert.ToInt32(quote.NumRatings);
            phrase.RatingScore = Convert.ToInt32(quote.RatingScore);
            return phrase;
        }
        private QuoteDM Map(QuoteSM quote)
        {
            QuoteDM phrase = new QuoteDM();
            phrase.QuoteId = quote.QuoteId.ToString();
            phrase.Phrase = quote.Phrase;
            phrase.Author = quote.Author;
            phrase.Category = quote.CategoryNum.ToString();
            phrase.Rating = quote.RatingNum.ToString();
            phrase.NumRatings = quote.NumRatings.ToString();
            phrase.RatingScore = quote.RatingScore.ToString();
            return phrase;
        }
        private List<QuoteSM> Map(List<QuoteDM> library)
        {
            List<QuoteSM> quotes = new List<QuoteSM>();
            foreach (QuoteDM quote in library)
            {
                quotes.Add(Map(quote));
            }
            return quotes;
        }
        private List<QuoteDM> Map(List<QuoteSM> library)
        {
            List<QuoteDM> quotes = new List<QuoteDM>();
            foreach (QuoteSM quote in library)
            {
                quotes.Add(Map(quote));
            }
            return quotes;
        }
        public void CreateQuote(QuoteSM quote)
        {
            try
            {
                quote.RatingNum = 0;
                quoteData.CreateQuote(Map(quote));
                logs.LogError("Event ", "User was able to create new item ", "Class:QuoteLogic, Method:CreateQuote");
            }
            catch (Exception P)
            {
                logs.LogError("Error ", "User was unable to create a new item ", "Class:UserLogic, Method:CreateQuote");
            }
        }
        public QuoteSM GetQuoteById(int tempId) //Finds a Item in the Inventory
        {
            return Map(quoteData.GetQuoteById(tempId));
        }
        public void RemoveQuoteById(int quoteId)
        {
            try
            {
                quoteData.RemoveQuoteById(quoteId);
                logs.LogError("Event ", "User was able to remove Quote", "Class:QuoteLogic, Method:RemoveQuoteById");
            }
            catch (Exception z)
            {
                logs.LogError("Error ", "User was unable to remove Quote", "Class:QuoteLogic, Method:RemoveQuoteById");
            }
        }
        public void EditQuoteById(QuoteSM quote)
        {
            try
            {
                if (quote.NumRatings > 0)
                {
                    quote.RatingNum = (quote.RatingScore / quote.NumRatings);
                }
                quoteData.EditQuoteById(Map(quote));
                logs.LogError("Event ", "User was able to update Quote", "Class:QuoteLogic, Method:UpdateQuoteById");
            }
            catch(Exception e) 
            {
                logs.LogError("Error ", "User was unable to update Quote", "Class:QuoteLogic, Method:UpdateQuoteById");
            }
        }
    }
}
