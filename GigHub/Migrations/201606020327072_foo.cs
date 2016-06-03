namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foo : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Attendances", new[] { "GigId" });
            DropIndex("dbo.Attendances", new[] { "Gig_Id" });
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropIndex("dbo.UserNotifications", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Attendances", "GigId");
            DropColumn("dbo.UserNotifications", "UserId");
            RenameColumn(table: "dbo.UserNotifications", name: "ApplicationUser_Id", newName: "UserId");
            RenameColumn(table: "dbo.Attendances", name: "Gig_Id", newName: "GigId");
            DropPrimaryKey("dbo.Attendances");
            DropPrimaryKey("dbo.UserNotifications");
            AlterColumn("dbo.Attendances", "GigId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserNotifications", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Attendances", new[] { "GigId", "AttendeeId" });
            AddPrimaryKey("dbo.UserNotifications", new[] { "UserId", "NotificationId" });
            CreateIndex("dbo.Attendances", "GigId");
            CreateIndex("dbo.UserNotifications", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropIndex("dbo.Attendances", new[] { "GigId" });
            DropPrimaryKey("dbo.UserNotifications");
            DropPrimaryKey("dbo.Attendances");
            AlterColumn("dbo.UserNotifications", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Attendances", "GigId", c => c.Int());
            AddPrimaryKey("dbo.UserNotifications", new[] { "UserId", "NotificationId" });
            AddPrimaryKey("dbo.Attendances", new[] { "GigId", "AttendeeId" });
            RenameColumn(table: "dbo.Attendances", name: "GigId", newName: "Gig_Id");
            RenameColumn(table: "dbo.UserNotifications", name: "UserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.UserNotifications", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Attendances", "GigId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserNotifications", "ApplicationUser_Id");
            CreateIndex("dbo.UserNotifications", "UserId");
            CreateIndex("dbo.Attendances", "Gig_Id");
            CreateIndex("dbo.Attendances", "GigId");
        }
    }
}
