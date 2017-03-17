using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IQuoteDAO
    {
        void GetDataWriter(ISQLDAO dataWriter);
        List<QuoteDM> Read(SqlParameter[] parameters, string statement);
        List<QuoteDM> GetQuotes();
        void CreateQuote(QuoteDM quote);
        void RemoveQuoteById(int quoteId);
        void EditQuoteById(QuoteDM quote);
        QuoteDM GetQuoteById(int quoteId);
    }
}
