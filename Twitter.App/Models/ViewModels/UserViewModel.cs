﻿using System;
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
                    info = new UserInfoViewModel(){
                        Id = u.Id,
                        favouritesCount = u.FavouriteTweets.Count(),
                        fallowerCount = u.Followers.Count(),
                        fallowingCount = u.Following.Count(),
                        tweetsCount = u.Tweets.Select(t => t.Id)
                            .Union(u.Retweeted.Select(r => r.Id)).Count()
                    }
                };
            }
        }

        public string Id { get; set; }
        public UserInfoViewModel info { get; set; }
        public IEnumerable<TweetIndexViewModel> tweets { get; set; }
    }
}