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
            var users = this.Data.Users.All().Where(u => u.UserName == User.Identity.Name).Select(u => u.Following.Select(f => f.Id)).FirstOrDefault();
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.pagesCount = (int)Math.Ceiling((double)this.Data.Tweets.All().Where(t => users.Contains(t.UserId)).Count() / PAGE_SIZE);
            }
            else
            {
                ViewBag.pagesCount = (int) Math.Ceiling((double)this.Data.Tweets.All().Count() / PAGE_SIZE);
            }
            
            if (id < 0)
            {
                id = 0;
            }
            else if (id >= ViewBag.pagesCount)
            {
                id = ViewBag.pagesCount - 1;
            }

            IEnumerable<TweetIndexViewModel> tweets = null;
            if (User.Identity.IsAuthenticated)
            {
                //tweets = this.Data.Users.All().Where(u => u.UserName == User.Identity.Name).Select(u => u.Following.Select(f => f.Tweets.Select(t => new TweetIndexViewModel
                //{
                //    Id = t.Id,
                //    Content = t.Content,
                //    SendOn = t.SendOn,
                //    UserId = t.UserId
                //}))).ToList();
                
                tweets = this.Data.Tweets.All().Where(t => users.Contains(t.UserId))
                    .OrderByDescending(t => t.SendOn)
                    .Skip(id * PAGE_SIZE)
                    .Take(PAGE_SIZE)
                    .Select(TweetIndexViewModel.Create).ToList();
            }
            else
            {
                tweets = this.Data.Tweets.All().OrderByDescending(t => t.SendOn).Skip(id * PAGE_SIZE).Take(PAGE_SIZE).Select(TweetIndexViewModel.Create).ToList();
            }
          
            ViewBag.page = id + 1;
            
            return View(tweets);
        }

        [Authorize]
        public ActionResult profile(string id)
        {
            var user = this.Data.Users.All().Where(u => u.Id == id).Select(UserViewModel.Create).FirstOrDefault();
            return View(user);
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