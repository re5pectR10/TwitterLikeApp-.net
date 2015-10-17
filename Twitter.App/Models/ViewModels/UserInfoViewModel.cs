using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter.App.Models.ViewModels
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }
        public int favouritesCount { get; set; }
        public int tweetsCount { get; set; }
        public int fallowingCount { get; set; }
        public int fallowerCount { get; set; }
    }
}