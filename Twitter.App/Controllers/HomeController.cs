using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.App.Models.ViewModels;
using Twitter.Data.Contracts;

namespace Twitter.App.Controllers
{
    public class HomeController : BaseController
    {
        private const int PAGE_SIZE = 3;

        public HomeController()
            :this(new Twitter.Data.TwitterData())
        { 
        }

        public HomeController(ITwitterData data)
            :base(data)
        {
        }

        public ActionResult Index(int id = 1)
        {
            id--;
            ViewBag.pagesCount = (int) Math.Ceiling((double)this.Data.Tweets.All().Count() / PAGE_SIZE);
            if (id < 0)
            {
                id = 0;
            }
            else if (id >= ViewBag.pagesCount)
            {
                id = ViewBag.pagesCount - 1;
            }
           
            var tweets = this.Data.Tweets.All().OrderByDescending(t=>t.SendOn).Skip(id*PAGE_SIZE).Take(PAGE_SIZE).Select(TweetIndexViewModel.Create);
            ViewBag.page = id + 1;
            
            return View(tweets);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}