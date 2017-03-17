using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IQuoteLogic
    {
        List<QuoteSM> GetQuotes();
        void CreateQuote(QuoteSM quote);
        QuoteSM GetQuoteById(int tempId);
        void RemoveQuoteById(int quoteId);
        void EditQuoteById(QuoteSM quote);
    }
}
