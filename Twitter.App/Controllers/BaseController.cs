using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.Data.Contracts;

namespace Twitter.App.Controllers
{
    public class BaseController : Controller
    {
        private ITwitterData data;

        protected ITwitterData Data { get { return this.data; } }

        public BaseController(ITwitterData data)
        {
            this.data = data;
        }
    }
}