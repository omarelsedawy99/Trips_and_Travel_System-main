using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace trips_and_travel_system.Models
{
    public class TripsAndTravelContext : DbContext
    {
        public DbSet<User> Users { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<FAQ> FAQs { set; get; }
        public DbSet<Role> Roles { set; get; }
        public DbSet<Agency> Agencies { set; get; }

        public TripsAndTravelContext() : base("name=DatabaseContext")
        {
            Database.SetInitializer<TripsAndTravelContext>(new CreateDatabaseIfNotExists<TripsAndTravelContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region User Configration

            modelBuilder.Entity<User>()
                .HasKey<int>(u => u.UserId);
            modelBuilder.Entity<User>()
                .HasMany<Post>(u => u.SavedPosts)
                .WithMany(p => p.UserPosts)
                .Map(userSavedPosts =>
                        {
                            userSavedPosts.MapLeftKey("UserId");
                            userSavedPosts.MapRightKey("SavedPostId");
                            userSavedPosts.ToTable("UserSavedPosts");
                        });
            modelBuilder.Entity<User>()
                .HasMany<Post>(t => t.likedPosts)
                .WithMany(p => p.likedTravelers)
                .Map(travelerLikedPosts =>
                {
                    travelerLikedPosts.MapLeftKey("TravelerId");
                    travelerLikedPosts.MapRightKey("LikedPostId");
                    travelerLikedPosts.ToTable("TravelerLikedPosts");
                });
            modelBuilder.Entity<User>()
                .HasMany<Post>(t => t.dislikedPosts)
                .WithMany(p => p.dislikedTravelers)
                .Map(travelerDislikedPosts =>
                {
                    travelerDislikedPosts.MapLeftKey("TravelerId");
                    travelerDislikedPosts.MapRightKey("DislikedPostId");
                    travelerDislikedPosts.ToTable("TravelerDislikedPosts");
                });
            modelBuilder.Entity<User>()
                .HasOptional(u => u.agency)
                .WithRequired(a => a.user);

            #endregion

            #region Post Configration

            modelBuilder.Entity<Post>()
                .HasKey<int>(p => p.PostId);
            modelBuilder.Entity<Post>()
                .HasMany(p => p.fAQs)
                .WithRequired(faq => faq.post)
                .HasForeignKey(faq => faq.postId);

            #endregion

            #region FAQ Configration

            modelBuilder.Entity<FAQ>()
                .HasKey<int>(faq => faq.FAQId);

            #endregion

            #region RoleMaster Configration

            modelBuilder.Entity<Role>()
                .HasKey<int>(r => r.RoleId);
            modelBuilder.Entity<Role>()
                .HasMany(r => r.users)
                .WithRequired(u => u.role)
                .HasForeignKey(u => u.roleId);

            #endregion

            #region AgencyConfigration

            modelBuilder.Entity<Agency>()
                .HasMany(a => a.TripPosts)
                .WithRequired(p => p.Agency)
                .HasForeignKey(p => p.AgencyId);

            #endregion

        }
    }
}