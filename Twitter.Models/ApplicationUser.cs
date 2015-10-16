using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Models
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<Tweet> tweets;
        private ICollection<Retweeted> retweeted;
        private ICollection<ApplicationUser> followers;
        private ICollection<ApplicationUser> following;
        private ICollection<Message> sendMessages;
        private ICollection<Message> receivedMessages;
        private ICollection<Tweet> favouriteTweets;
        private ICollection<Notification> notifications;

        public ApplicationUser()
        {
            this.tweets = new HashSet<Tweet>();
            this.retweeted = new HashSet<Retweeted>();
            this.followers = new HashSet<ApplicationUser>();
            this.following = new HashSet<ApplicationUser>();
            this.sendMessages = new HashSet<Message>();
            this.receivedMessages = new HashSet<Message>();
            this.favouriteTweets = new HashSet<Tweet>(); 
            this.notifications = new HashSet<Notification>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string Bio { get; set; }
        public DateTime Joined { get; set; }
        public virtual ICollection<Tweet> Tweets { get { return this.tweets; } set { this.tweets = value; } }
        public virtual ICollection<Retweeted> Retweeted { get { return this.retweeted; } set { this.retweeted = value; } }
        public virtual ICollection<ApplicationUser> Followers { get { return this.followers; } set { this.followers = value; } }
        public virtual ICollection<ApplicationUser> Following { get { return this.following; } set { this.following = value; } }
        [InverseProperty("Sender")]
        public virtual ICollection<Message> SendMessages { get { return this.sendMessages; } set { this.sendMessages = value; } }
        [InverseProperty("Receiver")]
        public virtual ICollection<Message> ReceivedMessages { get { return this.receivedMessages; } set { this.receivedMessages = value; } }
        public virtual ICollection<Tweet> FavouriteTweets { get { return this.favouriteTweets; } set { this.favouriteTweets = value; } }
        public virtual ICollection<Notification> Notifications { get { return this.notifications; } set { this.notifications = value; } }
    }
}
