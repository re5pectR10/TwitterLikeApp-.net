using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.Data.Contracts;
using Twitter.Models;

namespace Twitter.App.Controllers
{
    public class TweetsController : BaseController
    {
        public TweetsController()
            :this(new Twitter.Data.TwitterData())
        { 
        }

        public TweetsController(ITwitterData data)
            :base(data)
        {
        }

        // GET: Tweets
        [Authorize]
        public ActionResult post(string content)
        {
            var tweet = new Tweet()
            {
                Content = content,
                SendOn = DateTime.Now,
                UserId = this.Data.Users.All().Where(u => u.UserName == User.Identity.Name).FirstOrDefault().Id
            };
            this.Data.Tweets.Add(tweet);
            this.Data.SaveChanges();
            return RedirectToAction("index", "Home");
        }
    }
}