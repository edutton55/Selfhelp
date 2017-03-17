using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IPrevRateLogic
    {
        List<PrevRateSM> GetPrevRateByQuote(int quoteId);
        void AddPrevRate(int user, int quote, int seqNum);
        bool CheckPrevRate(int userId, int quoteId);
    }
}
