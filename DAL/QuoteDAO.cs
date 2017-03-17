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
    public class QuoteDAO : IQuoteDAO
    {
        private ILoggerIO logs = new LoggerIO();
        private ISQLDAO dataWriter = new SQLDAO();
        private string connectionString = @"Server=.\SQLEXPRESS;Database=SelfHelp;Trusted_Connection=true;";
        public QuoteDAO(ILoggerIO log)
        {
            logs = log;
        }
        public void GetDataWriter(ISQLDAO dataWriter)
        {
            this.dataWriter = dataWriter;
        }
        public List<QuoteDM> Read(SqlParameter[] parameters, string statement)
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
                    List<QuoteDM> quotes = new List<QuoteDM>();
                    while (data.Read())
                    {
                        QuoteDM quote = new QuoteDM();
                        quote.QuoteId = data["QuoteId"].ToString();
                        quote.Phrase = data["Phrase"].ToString();
                        quote.Author = data["Author"].ToString();
                        quote.Category = data["Category"].ToString();
                        quote.Rating = data["Rating"].ToString();
                        quote.NumRatings = data["NumRatings"].ToString();
                        quote.RatingScore = data["RatingScore"].ToString();
                        quotes.Add(quote);
                    }
                    return quotes;
                }
            }
        }
        public List<QuoteDM> GetQuotes()
        {
            return Read(null, "GetQuotes");
        }
        public void CreateQuote(QuoteDM quote)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Phrase", quote.Phrase)
                , new SqlParameter("@Author", quote.Author)
                , new SqlParameter("@Category", quote.Category)
                , new SqlParameter("@Rating", quote.Rating)
                , new SqlParameter("@NumRatings", quote.NumRatings)
                , new SqlParameter("@RatingScore", quote.RatingScore)
            };
            dataWriter.Write(parameters, "CreateQuote");
            logs.LogError("Event", "A quote has been created", "Class:QuoteDAO, Method: CreateQuote");
        }
        public void RemoveQuoteById(int quoteId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@QuoteId", quoteId)
            };
            dataWriter.Write(parameters, "RemoveQuote");
            logs.LogError("Event", "An quote has been removed from the library", "Class:QuoteDAO, Method: RemoveQuote");
        }
        public void EditQuoteById(QuoteDM quote)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Phrase", quote.Phrase)
                , new SqlParameter("@Author", quote.Author)
                , new SqlParameter("@Category", quote.Category)
                , new SqlParameter("@Rating", quote.Rating)
                , new SqlParameter("@NumRatings", quote.NumRatings)
                , new SqlParameter("@RatingScore", quote.RatingScore)
                , new SqlParameter("@Id", quote.QuoteId)
            };
            dataWriter.Write(parameters, "UpdateQuote");
            logs.LogError("Event", "An quote has been updated", "Class: QuoteDAO, Method: UpdateQuote");
        }
        public QuoteDM GetQuoteById(int quoteId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@QuoteId", quoteId)
            };
            return Read(parameters, "GetQuoteById").SingleOrDefault();
        }
    }
}
