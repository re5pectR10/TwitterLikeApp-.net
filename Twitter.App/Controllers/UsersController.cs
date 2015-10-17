using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.App.Models;
using Twitter.App.Models.ViewModels;
using Twitter.Data;
using Twitter.Data.Contracts;

namespace Twitter.App.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController()
            : this(new TwitterData())
        {

        }

        public UsersController(ITwitterData data)
            :base(data)
        {

        }

        [Authorize]
        public ActionResult profile(string id)
        {
            var user = this.Data.Users.All().Where(u => u.Id == id).Select(UserViewModel.Create).FirstOrDefault();
            user.tweets = this.Data.Tweets.All().Select(t => new TweetIndexViewModel()
            {
                Id = t.Id,
                SendOn = t.SendOn,
                Content = t.Content,
                UserId = t.UserId,
                Username = t.User.UserName
            }).Union(this.Data.Retweets.All().Where(u => u.UserId == user.Id).Select(r => new TweetIndexViewModel()
            {
                Id = r.Tweet.Id,
                SendOn = r.RetweetedOn,
                Content = r.Tweet.Content,
                UserId = r.Tweet.UserId,
                Username = r.Tweet.User.UserName
            })).OrderByDescending(t => t.SendOn);

            return View(user);
        }

        public ActionResult getFavourites(string id)
        {
            var userQuerry = this.Data.Users.All().Where(us => us.Id == id);
            var userModel = userQuerry.Select(UserViewModel.Create).FirstOrDefault();
            var user = userQuerry.FirstOrDefault();
            userModel.tweets = user.FavouriteTweets.Select(f => new TweetIndexViewModel()
            {
                Id = f.Id,
                SendOn = f.SendOn,
                Content = f.Content,
                UserId = f.UserId,
                Username = f.User.UserName
            });

            return View("profile", userModel);
        }

        public ActionResult getFollowing(string id)
        {
            var userQuerry = this.Data.Users.All().Where(us => us.Id == id);
            var userModel = userQuerry.Select(UserFollowerViewModel.Create).FirstOrDefault();
            var user = userQuerry.FirstOrDefault();
            userModel.follow = user.Following.Select(f => new IndexViewModel()
            {
                Id = f.Id,
                Name = f.UserName
            });

            return View("profileFollow", userModel);
        }

        public ActionResult getFollower(string id)
        {
            var userQuerry = this.Data.Users.All().Where(us => us.Id == id);
            var userModel = userQuerry.Select(UserFollowerViewModel.Create).FirstOrDefault();
            var user = userQuerry.FirstOrDefault();
            userModel.follow = user.Followers.Select(f => new IndexViewModel()
            {
                Id = f.Id,
                Name = f.UserName
            });

            return View("profileFollow", userModel);
        }
    }
}