using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class QuoteSM
    {
        public int QuoteId { get; set; }
        public string Phrase { get; set; }
        public string Author { get; set; }
        public int CategoryNum { get; set; }
        public int RatingNum { get; set; }
        public int NumRatings { get; set; }
        public int RatingScore { get; set; }
    }
}
