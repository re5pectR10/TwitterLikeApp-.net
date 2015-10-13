using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Twitter.Models;

namespace Twitter.App.Models.ViewModels
{
    public class UserViewModel
    {
        public static Expression<Func<ApplicationUser, UserViewModel>> Create
        {
            get
            {
                return u => new UserViewModel
                {
                    Id = u.Id,
                    favouritesCount = u.FavouriteTweets.Count(),
                    fallowerCount = u.Followers.Count(),
                    fallowingCount = u.Following.Count(),
                    tweets = u.Tweets.Select(t => new TweetIndexViewModel()
                    {
                        Id = t.Id,
                        SendOn = t.SendOn,
                        Content = t.Content,
                        UserId = t.UserId,
                        Username = t.User.UserName
                    }).Union(u.Retweeted.Select(r => new TweetIndexViewModel()
                    {
                        Id = r.Tweet.Id,
                        SendOn = r.RetweetedOn,
                        Content = r.Tweet.Content,
                        UserId = r.Tweet.UserId,
                        Username = r.User.UserName
                    })),
                };
            }
        }

        public string Id { get; set; }
        public int favouritesCount { get; set; }
        //public int tweetsCount { get; set; }
        public int fallowingCount { get; set; }
        public int fallowerCount { get; set; }
        public IEnumerable<TweetIndexViewModel> tweets { get; set; }
    }
}