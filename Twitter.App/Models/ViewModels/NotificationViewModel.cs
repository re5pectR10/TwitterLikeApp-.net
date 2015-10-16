using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Twitter.Models;

namespace Twitter.App.Models.ViewModels
{
    public class NotificationViewModel
    {
        public static Expression<Func<Notification, NotificationViewModel>> Create
        {
            get
            {
                return t => new NotificationViewModel
                {
                    Id = t.Id,
                    Content = t.Content,
                    SendOn = t.SendOn
                };
            }
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SendOn { get; set; }
    }
}