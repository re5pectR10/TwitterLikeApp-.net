using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.Data.Contracts;
using Twitter.Models;
using Microsoft.AspNet.Identity;
using Twitter.App.Models.ViewModels;

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

        [Authorize]
        public ActionResult retweet(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = this.Data.Users.All().FirstOrDefault(u => u.Id == userId);
            var tweet = this.Data.Tweets.All().FirstOrDefault(t => t.Id == id);
            if (tweet == null)
            {
                // todo: set error message
                return RedirectToAction("index", "home");
            }

            var retweet = new Retweeted()
            {
                TweetId = tweet.Id,
                UserId = user.Id,
                RetweetedOn = DateTime.Now
            };
            this.Data.Retweets.Add(retweet);
            this.Data.SaveChanges();
            return RedirectToAction("index", "home");
        }

        [Authorize]
        public ActionResult report(int id)
        {
            var tweet = this.Data.Tweets.All().FirstOrDefault(t => t.Id == id);
            if (tweet == null)
            {
                // todo: set error message
                return RedirectToAction("index", "home");
            }

            tweet.IsReported = true;
            this.Data.SaveChanges();
            return RedirectToAction("index", "home");
        }

        public ActionResult getReply(int id)
        {
            var tweet = this.Data.Tweets.All().Where(t => t.Id == id).Select(TweetDetailsViewModel.Create).FirstOrDefault();
            if (tweet == null)
            {
                // todo: set error message
                return RedirectToAction("index", "home");
            }

            return View("Reply", tweet);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Reply(int id, string content)
        {
            var tweet = this.Data.Tweets.All().FirstOrDefault(t => t.Id == id);
            if (tweet == null)
            {
                // todo: set error message
                return RedirectToAction("index", "home");
            }

            var replyTweet = new Tweet()
            {
                Content = content,
                SendOn = DateTime.Now,
                UserId = User.Identity.GetUserId(),
                RepliedTweetId = tweet.Id
            };

            this.Data.Tweets.Add(replyTweet);
            this.Data.SaveChanges();

            return RedirectToAction("getReply", new { id = tweet.Id });
        }
    }
}