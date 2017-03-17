using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class QuoteDM
    {
        public string QuoteId { get; set; }
        public string Phrase { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Rating { get; set; }
        public string NumRatings { get; set; }
        public string RatingScore { get; set; }
    }
}
