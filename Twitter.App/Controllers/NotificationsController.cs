using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.Data.Contracts;
using Microsoft.AspNet.Identity;
using Twitter.App.Models.ViewModels;

namespace Twitter.App.Controllers
{
    public class NotificationsController : BaseController
    {
        public NotificationsController()
            :this(new Twitter.Data.TwitterData())
        { 
        }

        public NotificationsController(ITwitterData data)
            :base(data)
        {
        }

        // GET: Notifications
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var notifications = this.Data.Notifications.All().Where(n => n.UserId == userId).Select(NotificationViewModel.Create);
            return View(notifications);
        }

        public ActionResult Details(int id)
        {
            var userId = User.Identity.GetUserId();
            var notifications = this.Data.Notifications.All().Where(n => n.UserId == userId && n.Id == id).Select(NotificationViewModel.Create);
            return View(notifications);
        }
    }
}