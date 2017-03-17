using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ISQLDAO
    {
        int Write(SqlParameter[] parameters, string statement);
    }
}
