using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Twitter.Models;

namespace Twitter.App.Models.ViewModels
{
    public class TweetDetailsViewModel
    {
        public static Expression<Func<Tweet, TweetDetailsViewModel>> Create
        {
            get
            {
                return t => new TweetDetailsViewModel
                {
                    Id = t.Id,
                    Content = t.Content,
                    SendOn = t.SendOn,
                    UserId = t.UserId,
                    Username = t.User.UserName,
                    ReplyTweets = t.ReplyTweets.Select(r => new TweetIndexViewModel()
                    { 
                        Id=r.Id,
                        Content=r.Content,
                        SendOn=r.SendOn,
                        UserId=r.UserId,
                        Username=r.User.UserName
                    })
                };
            }
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SendOn { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public IEnumerable<TweetIndexViewModel> ReplyTweets { get; set; }
    }
}