using Twitter.Models;

namespace Twitter.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TwitterDbContext : IdentityDbContext<ApplicationUser>
    {
        public TwitterDbContext()
            : base("name=Twitter.Data", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TwitterDbContext, Migrations.Configuration>());
        }

        public static TwitterDbContext Create()
        {
            return new TwitterDbContext();
        }

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Retweeted> Retweets { get; set; }
        public virtual DbSet<Tweet> Tweets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
              .HasMany(x => x.Following)
              .WithMany(x => x.Followers)
              .Map(m =>
              {
                  m.MapLeftKey("FollowingId");
                  m.MapRightKey("FollowerId");
                  m.ToTable("UserFollow");
              });

            modelBuilder.Entity<ApplicationUser>()
            . HasMany(c => c.FavouriteTweets)
            .WithMany(p => p.FavouriteTo)
            . Map(
              m =>
              {
                  m.MapLeftKey("UserId");
                  m.MapRightKey("TweetId");
                  m.ToTable("FavouriteTweets");
              });

            base.OnModelCreating(modelBuilder);
        }
    }
}