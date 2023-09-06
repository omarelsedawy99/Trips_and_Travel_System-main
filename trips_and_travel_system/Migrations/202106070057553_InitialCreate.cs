namespace trips_and_travel_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agencies",
                c => new
                    {
                        AgencyId = c.Int(nullable: false),
                        agencyName = c.String(),
                    })
                .PrimaryKey(t => t.AgencyId)
                .ForeignKey("dbo.Users", t => t.AgencyId)
                .Index(t => t.AgencyId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        details = c.String(),
                        tripDestination = c.String(),
                        tripImage = c.String(),
                        tripPrice = c.Single(nullable: false),
                        tripDate = c.DateTime(nullable: false),
                        postDate = c.DateTime(nullable: false),
                        accepted = c.Boolean(nullable: false),
                        AgencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Agencies", t => t.AgencyId, cascadeDelete: true)
                .Index(t => t.AgencyId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        email = c.String(),
                        password = c.String(),
                        firstName = c.String(),
                        lastName = c.String(),
                        phone = c.String(),
                        photo = c.String(),
                        roleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.roleId, cascadeDelete: true)
                .Index(t => t.roleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.FAQs",
                c => new
                    {
                        FAQId = c.Int(nullable: false, identity: true),
                        question = c.String(),
                        answer = c.String(),
                        postId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FAQId)
                .ForeignKey("dbo.Posts", t => t.postId, cascadeDelete: true)
                .Index(t => t.postId);
            
            CreateTable(
                "dbo.TravelerDislikedPosts",
                c => new
                    {
                        TravelerId = c.Int(nullable: false),
                        DislikedPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TravelerId, t.DislikedPostId })
                .ForeignKey("dbo.Users", t => t.TravelerId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.DislikedPostId, cascadeDelete: true)
                .Index(t => t.TravelerId)
                .Index(t => t.DislikedPostId);
            
            CreateTable(
                "dbo.TravelerLikedPosts",
                c => new
                    {
                        TravelerId = c.Int(nullable: false),
                        LikedPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TravelerId, t.LikedPostId })
                .ForeignKey("dbo.Users", t => t.TravelerId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.LikedPostId, cascadeDelete: true)
                .Index(t => t.TravelerId)
                .Index(t => t.LikedPostId);
            
            CreateTable(
                "dbo.UserSavedPosts",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        SavedPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.SavedPostId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.SavedPostId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SavedPostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "AgencyId", "dbo.Agencies");
            DropForeignKey("dbo.FAQs", "postId", "dbo.Posts");
            DropForeignKey("dbo.UserSavedPosts", "SavedPostId", "dbo.Posts");
            DropForeignKey("dbo.UserSavedPosts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "roleId", "dbo.Roles");
            DropForeignKey("dbo.TravelerLikedPosts", "LikedPostId", "dbo.Posts");
            DropForeignKey("dbo.TravelerLikedPosts", "TravelerId", "dbo.Users");
            DropForeignKey("dbo.TravelerDislikedPosts", "DislikedPostId", "dbo.Posts");
            DropForeignKey("dbo.TravelerDislikedPosts", "TravelerId", "dbo.Users");
            DropForeignKey("dbo.Agencies", "AgencyId", "dbo.Users");
            DropIndex("dbo.UserSavedPosts", new[] { "SavedPostId" });
            DropIndex("dbo.UserSavedPosts", new[] { "UserId" });
            DropIndex("dbo.TravelerLikedPosts", new[] { "LikedPostId" });
            DropIndex("dbo.TravelerLikedPosts", new[] { "TravelerId" });
            DropIndex("dbo.TravelerDislikedPosts", new[] { "DislikedPostId" });
            DropIndex("dbo.TravelerDislikedPosts", new[] { "TravelerId" });
            DropIndex("dbo.FAQs", new[] { "postId" });
            DropIndex("dbo.Users", new[] { "roleId" });
            DropIndex("dbo.Posts", new[] { "AgencyId" });
            DropIndex("dbo.Agencies", new[] { "AgencyId" });
            DropTable("dbo.UserSavedPosts");
            DropTable("dbo.TravelerLikedPosts");
            DropTable("dbo.TravelerDislikedPosts");
            DropTable("dbo.FAQs");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Posts");
            DropTable("dbo.Agencies");
        }
    }
}
