using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Models
{
    public class Tweet
    {
        private ICollection<ApplicationUser> favouriteTo;
        private ICollection<Tweet> replyTweets;
        private ICollection<Retweeted> retweetedTo;

        public Tweet()
        {
            this.favouriteTo = new HashSet<ApplicationUser>();
            this.replyTweets = new HashSet<Tweet>();
            this.retweetedTo = new HashSet<Retweeted>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime SendOn { get; set; }
        public bool IsReported { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int? RepliedTweetId { get; set; }
        public virtual Tweet RepliedTweet { get; set; }
        public virtual ICollection<ApplicationUser> FavouriteTo { get { return this.favouriteTo; } set { this.favouriteTo = value; } }
        public virtual ICollection<Tweet> ReplyTweets { get { return this.replyTweets; } set { this.replyTweets = value; } }
        public virtual ICollection<Retweeted> RetweetedTo { get { return this.retweetedTo; } set { this.retweetedTo = value; } }
    }
}
