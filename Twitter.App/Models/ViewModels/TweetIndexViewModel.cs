using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Twitter.Models;

namespace Twitter.App.Models.ViewModels
{
    public class TweetIndexViewModel
    {
        public static Expression<Func<Tweet, TweetIndexViewModel>> Create
        {
            get
            {
                return t => new TweetIndexViewModel
                {
                    Id = t.Id,
                    Content = t.Content,
                    SendOn = t.SendOn,
                    UserId = t.UserId,
                    Username = t.User.UserName
                };
            }
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SendOn { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}