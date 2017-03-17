using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpConsciousness.Models
{
    public class RatingVM
    {
        public int RateId { get; set; }
        public string Rating { get; set; }

        public static RatingSM Map(RatingVM rate)
        {
            RatingSM rating = new RatingSM();
            rating.RateId = rate.RateId;
            rating.Rating = rate.Rating;
            return rating;
        }
        public static RatingVM Map(RatingSM phrase)
        {
            RatingVM quote = new RatingVM();
            quote.RateId = phrase.RateId;
            quote.Rating = phrase.Rating;
            return quote;
        }
        public static List<RatingSM> Map(List<RatingVM> ratesList)
        {
            List<RatingSM> ratelist = new List<RatingSM>();
            foreach (RatingVM rating in ratesList)
            {
                ratelist.Add(Map(rating));
            }
            return ratelist;
        }
        public static List<RatingVM> Map(List<RatingSM> ratesList)
        {
            List<RatingVM> ratelist = new List<RatingVM>();
            foreach (RatingSM rating in ratesList)
            {
                ratelist.Add(Map(rating));
            }
            return ratelist;
        }
    }
}