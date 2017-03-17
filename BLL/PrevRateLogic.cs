using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ApplicationLogger;
using System.Data.SqlClient;

namespace BLL
{
    public class PrevRateLogic : IPrevRateLogic
    {
        private ILoggerIO logs;
        private IPrevRateDAO PrevRateData;
        private IHashing hash;

        public PrevRateLogic(IPrevRateDAO PrevRateDAO, ISQLDAO dao, ILoggerIO log, IHashing hasher)
        {
            hash = hasher;
            logs = log;
            PrevRateData = PrevRateDAO;   //injecting dependency
            PrevRateData.GetDataWriter(dao);//dependency injector through a infrastructure
        }
        /// <summary>
        /// Creates a List of PrevRateSM from Database
        /// </summary>
        /// <returns>List of PrevRateSM</returns>
        public List<PrevRateSM> GetPrevRateByQuote(int quoteId) //Creates PrevRateList
        {
            return Map(PrevRateData.GetPrevRateByQuote(quoteId));
        }
        /// <summary>
        /// Converts PrevRateDM to PrevRateSM for use on the Logic Layer
        /// </summary>
        /// <param name="prev">PrevRateDM human</param>
        /// <returns>PrevRateSM</returns>
        private PrevRateSM Map(PrevRateDM prev) // Converts for PrevRate in the Logic/Presentation Layer
        {
            PrevRateSM rate = new PrevRateSM();
            rate.QuoteId = Convert.ToInt32(prev.QuoteId);
            rate.SeqNum = Convert.ToInt32(prev.SeqNum);
            rate.UserId = Convert.ToInt32(prev.UserId);
            return rate;
        }
        /// <summary>
        /// Converts a PrevRateSM to a PrevRateDM
        /// </summary>
        /// <param name="human">PrevRateSM human</param>
        /// <returns>PrevRateDM</returns>
        private PrevRateDM Map(PrevRateSM prev) //Converts for use in the Data Layer
        {
            PrevRateDM rate = new PrevRateDM();
            rate.QuoteId = prev.QuoteId.ToString();
            rate.SeqNum = prev.SeqNum.ToString();
            rate.UserId = prev.UserId.ToString();
            return rate;
        }
        /// <summary>
        /// Converts a list of PrevRateDM to PrevRateSM
        /// </summary>
        /// <param name="human">List PrevRateDM PrevRates</param>
        /// <returns>List of PrevRateSM</returns>
        private List<PrevRateSM> Map(List<PrevRateDM> PrevRates) //Converts for Use in the Logic/Presentation Layer
        {
            List<PrevRateSM> people = new List<PrevRateSM>();
            foreach (PrevRateDM PrevRate in PrevRates)
            {
                people.Add(Map(PrevRate));
            }
            return people;
        }
        /// <summary>
        /// Converts a list of PrevRateSM to PrevRateDM
        /// </summary>
        /// <param name="human">List PrevRateSM PrevRates</param>
        /// <returns>List of PrevRateDM</returns>
        private List<PrevRateDM> Map(List<PrevRateSM> PrevRates) //Converts for use in the Data Layer
        {
            List<PrevRateDM> people = new List<PrevRateDM>();
            foreach (PrevRateSM PrevRate in PrevRates)
            {
                people.Add(Map(PrevRate));
            }
            return people;
        }
        /// <summary>
        /// Checks PrevRate Inputed PrevRateName Against the Database
        /// </summary>
        /// <param name="tempPrevRatename">string tempPrevRatename</param>
        /// <returns>bool</returns>
        public bool CheckPrevRate(int userId, int quoteId) //Matches PrevRateName Against PrevRateList
        {
            List<PrevRateSM> list = GetPrevRateByQuote(quoteId);
            foreach (PrevRateSM item in list)
            {
                if (quoteId == item.QuoteId && userId == item.UserId)
                {
                    return true;
                }
            }
            return false;
        }

        
        /// <summary>
        /// Adds PrevRate to DataBase based on PrevRate input.
        /// </summary>
        /// <param name="PrevRate">PrevRateSM PrevRate</param>
        public void AddPrevRate(int userId, int quoteId, int seqNum)
        {
            try
            {
                PrevRateSM prev = new PrevRateSM();
                prev.QuoteId = quoteId;
                prev.UserId = userId;
                prev.SeqNum = seqNum;
                if (!CheckPrevRate(prev.UserId, prev.QuoteId))
                {
                    PrevRateData.AddPrevRate(Map(prev));
                    logs.LogError("Event", "a new PrevRate has been been added to database", "Class:PrevRateLogic,Method:NewPrevRate");
                }
            }
            catch (Exception d)
            {
                logs.LogError("Error", "A new PrevRate has not been added to the database", "Class:PrevRateLogic,Method:NewPrevRate");
            }
        }



    }
}
