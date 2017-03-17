using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IPrevRateDAO
    {
        void GetDataWriter(ISQLDAO dataWriter);
        List<PrevRateDM> Read(SqlParameter[] parameters, string statement);
        List<PrevRateDM> GetPrevRateByQuote(int id);
        void AddPrevRate(PrevRateDM PrevRate);
        
    }
}
