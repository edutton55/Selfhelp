using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ApplicationLogger;

namespace BLL
{
    public class RatingLogic:IRatingLogic
    {
        private IRatingDAO ratingData;
        private static ILoggerIO logs;

        public RatingLogic(IRatingDAO ratingDAO, ISQLDAO dao, ILoggerIO log)
        {
            logs = log;
            ratingData = ratingDAO;   //injecting dependency
            ratingData.GetDataWriter(dao);//dependency injector through a infrastructure
        }
        public List<RatingSM> GetRatings()
        {
            return Map(ratingData.GetRatings());
        }
        private RatingSM Map(RatingDM rating)
        {
            RatingSM video = new RatingSM();
            video.RateId = Convert.ToInt32(rating.RateId);
            video.Rating = rating.Rating;
            return video;
        }
        private RatingDM Map(RatingSM rating)
        {
            RatingDM video = new RatingDM();
            video.RateId = rating.RateId.ToString();
            video.Rating = rating.Rating;
            return video;
        }
        private List<RatingSM> Map(List<RatingDM> ratings)
        {
            List<RatingSM> rates = new List<RatingSM>();
            foreach (RatingDM rating in ratings)
            {
                rates.Add(Map(rating));
            }
            return rates;
        }
        private List<RatingDM> Map(List<RatingSM> ratings)
        {
            List<RatingDM> rates = new List<RatingDM>();
            foreach (RatingSM rating in ratings)
            {
                rates.Add(Map(rating));
            }
            return rates;
        }
        //public void CreateRating(RatingSM rating)
        //{
        //    try
        //    {
        //        ratingData.CreateRating(Map(rating));
        //        logs.LogError("Event ", "User was able to create new item ", "Class:RatingLogic, Method:CreateRating");
        //    }
        //    catch (Exception P)
        //    {
        //        logs.LogError("Error ", "User was unable to create a new item ", "Class:UserLogic, Method:CreateRating");
        //    }
        //}
        public RatingSM GetRatingById(int tempId) 
        {
            return Map(ratingData.GetRatingById(tempId));
        }
        
        public void EditRatingById(RatingSM rating)
        {
            try
            {
                ratingData.EditRatingById(Map(rating));
                logs.LogError("Event ", "User was able to update Rating", "Class:RatingLogic, Method:UpdateRatingByRateId");
            }
            catch(Exception e) 
            {
                logs.LogError("Error ", "User was unable to update Rating", "Class:RatingLogic, Method:UpdateRatingByRateId");
            }
        }
        public string GetRating(int id)
        {
                RatingSM rating = Map(ratingData.GetRatingById(id));
                return rating.Rating;
        }
    }
}
