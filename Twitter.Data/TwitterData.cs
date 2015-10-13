using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Data.Contracts;

namespace Twitter.Data
{
    public class TwitterData : ITwitterData
    {
        private readonly DbContext context;

        private readonly IDictionary<Type, object> repositories;

        public TwitterData()
            : this(new TwitterDbContext())
        {
        }

        public TwitterData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public IRepository<Models.ApplicationUser> Users
        {
            get { return this.GetRepository<Models.ApplicationUser>(); }
        }

        public IRepository<Models.Tweet> Tweets
        {
            get { return this.GetRepository<Models.Tweet>(); }
        }

        public IRepository<Models.Message> Messages
        {
            get { return this.GetRepository<Models.Message>(); }
        }

        public IRepository<Models.Retweeted> Retweets
        {
            get { return this.GetRepository<Models.Retweeted>(); }
        }

        public IRepository<Models.Notification> Notifications
        {
            get { return this.GetRepository<Models.Notification>(); }
        }
    }
}
