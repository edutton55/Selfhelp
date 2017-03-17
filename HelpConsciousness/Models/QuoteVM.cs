using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpConsciousness.Models
{
    public class QuoteVM
    {
        

        public int QuoteId { get; set; }
        public string Phrase { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int CategoryNumber { get; set; }
        public string Rating { get; set; }
        public int RatingNumber { get; set; }
        public int NumRatings { get; set; }
        public int RatingScore { get; set; }
        public bool HasRated { get; set; }

        public static QuoteSM Map(QuoteVM quote)
        {
            QuoteSM phrase = new QuoteSM();
            phrase.QuoteId = quote.QuoteId;
            phrase.Phrase = quote.Phrase;
            phrase.Author = quote.Author;
            phrase.CategoryNum = quote.CategoryNumber;
            phrase.RatingNum = quote.RatingNumber;
            phrase.NumRatings = quote.NumRatings;
            phrase.RatingScore = quote.RatingScore;
            return phrase;
        }
        public static QuoteVM Map(QuoteSM phrase)
        {
            QuoteVM quote = new QuoteVM();
            quote.QuoteId = phrase.QuoteId;
            quote.Phrase = phrase.Phrase;
            quote.Author = phrase.Author;
            quote.CategoryNumber = phrase.CategoryNum;
            quote.RatingNumber = phrase.RatingNum;
            quote.NumRatings = phrase.NumRatings;
            quote.RatingScore = phrase.RatingScore;
            return quote;
        }
        public static List<QuoteSM> Map(List<QuoteVM> quotesList)
        {
            List<QuoteSM> quotelist = new List<QuoteSM>();
            foreach (QuoteVM quote in quotesList)   
            {
                quotelist.Add(Map(quote));
            }
            return quotelist;
        }
        public static List<QuoteVM> Map(List<QuoteSM> quotesList)
        {
            List<QuoteVM> quotelist = new List<QuoteVM>();
            foreach (QuoteSM quote in quotesList)
            {
                quotelist.Add(Map(quote));
            }
            return quotelist;
        }
    }
}