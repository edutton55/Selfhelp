using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRatingDAO
    {
        void GetDataWriter(ISQLDAO dataWriter);
        List<RatingDM> Read(SqlParameter[] parameters, string statement);
        List<RatingDM> GetRatings();
        void EditRatingById(RatingDM rating);
        RatingDM GetRatingById(int ratingId);
    }
}
