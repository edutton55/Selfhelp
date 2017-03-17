using ApplicationLogger;
using BLL;
using DAL;
using HelpConsciousness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpConsciousness.Controllers
{
    public class QuoteController : Controller
    {
        public IQuoteLogic quoteLogic = new QuoteLogic(new QuoteDAO(new LoggerIO()), new SQLDAO(), new LoggerIO());
        public IRatingLogic ratingLogic = new RatingLogic(new RatingDAO(new LoggerIO()), new SQLDAO(), new LoggerIO());
        public ICategoryLogic categoryLogic = new CategoryLogic(new CategoryDAO(new LoggerIO()), new SQLDAO(), new LoggerIO());
        public IPrevRateLogic prevRateLogic = new PrevRateLogic(new PrevRateDAO(new LoggerIO()), new SQLDAO(), new LoggerIO(), new Hashing());

        // GET: Quote
        public ActionResult Index()
        {
            List<QuoteVM> quotes = QuoteVM.Map(quoteLogic.GetQuotes());
            int userId = (int)Session["UserId"];
            foreach (QuoteVM quote in quotes)
            {
                quote.HasRated = prevRateLogic.CheckPrevRate(userId, quote.QuoteId);
                quote.Rating = ratingLogic.GetRating(quote.RatingNumber);
                quote.Category = categoryLogic.GetCategory(quote.CategoryNumber);
            }
            return View(quotes);
        }

        //GET: Quote/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Quote/Create
        [HttpPost]
        public ActionResult Create(QuoteVM quote)
        {
            try
            {
                quoteLogic.CreateQuote(QuoteVM.Map(quote));
                return RedirectToAction("AdminQuoteIndex");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            QuoteVM quote = QuoteVM.Map(quoteLogic.GetQuoteById(id));
            ViewBag.QuoteId = quote.QuoteId;
            ViewBag.Phrase = quote.Phrase;
            return View(quote);
        }

        //GET: Quote/Edit
        public ActionResult Edit(int id)
        {
            QuoteVM quote = QuoteVM.Map(quoteLogic.GetQuoteById(id));
            return View(quote);
        }

        //POST: Quote/Edit
        [HttpPost]
        public ActionResult Edit(QuoteVM quote, int id)
        {
            try
            {
                quote.QuoteId = id;
                quoteLogic.EditQuoteById(QuoteVM.Map(quote));
                return RedirectToAction("AdminQuoteIndex");
            }
            catch (Exception p)
            {
                return View();
            }
        }

        //GET: Quote/Delete
        public ActionResult Delete(int id)
        {
            QuoteVM quote = QuoteVM.Map(quoteLogic.GetQuoteById(id));
            return View(quote);
        }

        //POST: Quote/Delete
        [HttpPost]
        public ActionResult Delete(QuoteVM quote, int id)
        {
            try
            {
                quoteLogic.RemoveQuoteById(id);
                return RedirectToAction("AdminQuoteIndex");
            }
            catch (Exception o)
            {
                return View();
            }
        }

        // GET: Quote
        public ActionResult AdminQuoteIndex()
        {
            List<QuoteVM> quotes = QuoteVM.Map(quoteLogic.GetQuotes());
            foreach(QuoteVM quote in quotes)
            {
                quote.Rating = ratingLogic.GetRating(quote.RatingNumber);
                quote.Category = categoryLogic.GetCategory(quote.CategoryNumber);
            }
            return View(quotes);
        }

        //GET: QuoteByCategory
        public ActionResult QuoteByCategory(string category)
        {
            int userId = (int)Session["Userid"];
            Session["Category"] = category;
            List<QuoteVM> quotes = QuoteVM.Map(quoteLogic.GetQuotes());
            List<QuoteVM> qs = new List<QuoteVM>();
            foreach (QuoteVM quote in quotes)
            {
                quote.HasRated = prevRateLogic.CheckPrevRate(userId, quote.QuoteId);
                quote.Rating = ratingLogic.GetRating(quote.RatingNumber);
                quote.Category = categoryLogic.GetCategory(quote.CategoryNumber);
                if (quote.Category == category)
                {
                    qs.Add(quote);
                }
            }
            return View(qs);
        }

        //GET: UpdateRating
        public ActionResult UpdateRatingByCategory(int rateId, int id)
        {
            int userId = (int)Session["UserId"];
            QuoteVM quote = QuoteVM.Map(quoteLogic.GetQuoteById(id));
            if (!prevRateLogic.CheckPrevRate(userId, quote.QuoteId))
            {
                quote.NumRatings += 1;
                quote.RatingScore += rateId;
                List<PrevRateSM> prevs = prevRateLogic.GetPrevRateByQuote(quote.QuoteId);
                int seq = prevs.Count + 1;
                prevRateLogic.AddPrevRate(userId, quote.QuoteId, seq);
                quoteLogic.EditQuoteById(QuoteVM.Map(quote));

            }
            return RedirectToAction("QuoteByCategory", "Quote", new { category = Session["Category"] });
        }

        //GET: UpdateRating
        public ActionResult UpdateRating(int rateId, int id)
        {
            int userId = (int)Session["UserId"];
            QuoteVM quote = QuoteVM.Map(quoteLogic.GetQuoteById(id));
            if (!prevRateLogic.CheckPrevRate(userId, quote.QuoteId))
            {
                quote.NumRatings += 1;
                quote.RatingScore += rateId;
                List<PrevRateSM> prevs = prevRateLogic.GetPrevRateByQuote(quote.QuoteId);
                int seq = prevs.Count + 1;
                prevRateLogic.AddPrevRate(userId, quote.QuoteId, seq);
                quoteLogic.EditQuoteById(QuoteVM.Map(quote));

            }
            return RedirectToAction("Index", "Quote");
        }

        public ActionResult Articles()
        {
            return View();
        }
    }
}
