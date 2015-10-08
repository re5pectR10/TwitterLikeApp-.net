using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Models;

namespace Twitter.Data.Contracts
{
    public interface ITwitterData
    {
        IRepository<ApplicationUser> Users { get; }
        IRepository<Tweet> Tweets { get; }
        IRepository<Message> Messages { get; }
        IRepository<Retweeted> Retweets { get; }
        IRepository<Notification> Notifications { get; }
        Task<int> SaveChangesAsync();
    }
}
