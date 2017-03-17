using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLogger;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class PrevRateDAO : IPrevRateDAO
    {
        private ISQLDAO dataWriter;
        private ILoggerIO logs;
        private string connectionString = @"Server=.\SQLEXPRESS;Database = SelfHelp;Trusted_Connection=True;";

        public PrevRateDAO(ILoggerIO log)
        {
            logs = log;
        }
        public void GetDataWriter(ISQLDAO dataWriter)
        {
            this.dataWriter = dataWriter;
        }
        public List<PrevRateDM> Read(SqlParameter[] parameters, string statement)
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
                    List<PrevRateDM> prevs = new List<PrevRateDM>();
                    while (data.Read())
                    {
                        PrevRateDM prev = new PrevRateDM();
                        prev.QuoteId = data["QuoteId"].ToString();
                        prev.SeqNum = data["SeqNum"].ToString();
                        prev.UserId = data["UserId"].ToString();
                        prevs.Add(prev);
                    }
                    return prevs;
                }
            }
        }


        public List<PrevRateDM> GetPrevRateByQuote(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@QuoteId", id)
            };
            return Read(parameters, "GetRaters");
        }

        public void AddPrevRate(PrevRateDM PrevRate)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@QuoteId", PrevRate.QuoteId)
                ,new SqlParameter("@SeqNum", PrevRate.SeqNum)
                ,new SqlParameter("@UserId", PrevRate.UserId)
            };
            dataWriter.Write(parameters, "AddPrevRate");
            logs.LogError("Event", "An PrevRate has been added to the database", "Class:PrevRateDAO, Method:AddPrevRate");
        }

        

    }
}
