using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IRatingLogic
    {
        List<RatingSM> GetRatings();
        RatingSM GetRatingById(int tempId);
        void EditRatingById(RatingSM rating);
        string GetRating(int id);
    }
}
