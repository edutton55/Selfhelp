using ApplicationLogger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RatingDAO:IRatingDAO
    {
         private ILoggerIO logs = new LoggerIO();
        private ISQLDAO dataWriter = new SQLDAO();
        private string connectionString = @"Server=.\SQLEXPRESS;Database=SelfHelp;Trusted_Connection=true;";
        public RatingDAO(ILoggerIO log)
        {
            logs = log;
        }
        public void GetDataWriter(ISQLDAO dataWriter)
        {
            this.dataWriter = dataWriter;
        }
        public List<RatingDM> Read(SqlParameter[] parameters, string statement)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(statement, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);

                    }
                    connection.Open();
                    SqlDataReader data = command.ExecuteReader();
                    List<RatingDM> ratings = new List<RatingDM>();
                    while (data.Read())
                    {
                        RatingDM rating = new RatingDM();
                        rating.RateId = data["RateId"].ToString();
                        rating.Rating = data["Rating"].ToString();
                        ratings.Add(rating);
                    }
                    return ratings;
                }
            }
        }
        public List<RatingDM> GetRatings()
        {
            return Read(null, "GetRatings");
        }
        public void EditRatingById(RatingDM rating)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Id", rating.RateId)
                ,new SqlParameter("@RatingName", rating.Rating)
            };
            dataWriter.Write(parameters, "UpdateRating");
            logs.LogError("Event", "An rating has been updated", "Class: RatingDAO, Method: UpdateRating");
        }
        public RatingDM GetRatingById(int ratingId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@RateId", ratingId)
            };
            return Read(parameters, "GetRatingById").SingleOrDefault();
        }
    }
}
