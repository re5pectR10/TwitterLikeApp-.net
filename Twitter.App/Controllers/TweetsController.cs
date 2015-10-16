using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.Data.Contracts;
using Twitter.Models;
using Microsoft.AspNet.Identity;

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
            var userId = User.Identity.GetUserId();
            var tweet = new Tweet()
            {
                Content = content,
                SendOn = DateTime.Now,
                UserId = userId
            };
            this.Data.Tweets.Add(tweet);
            this.Data.SaveChanges();
            return RedirectToAction("index", "Home");
        }

        [Authorize]
        public ActionResult makeFavourite(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = this.Data.Users.All().FirstOrDefault(u => u.Id == userId);
            var tweet = this.Data.Tweets.All().FirstOrDefault(t => t.Id == id);
            if (tweet == null)
            {
                // todo: set error message
                return RedirectToAction("index", "home");
            }

            tweet.FavouriteTo.Add(user);
            this.Data.SaveChanges();
            return RedirectToAction("index", "home");
        }
    }
}