namespace DIANS_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class winery2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wineries", "ImageUrl", c => c.String());
            AddColumn("dbo.Wineries", "Raiting", c => c.Single(nullable: false));
            DropColumn("dbo.Wineries", "Latitude");
            DropColumn("dbo.Wineries", "Longitude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wineries", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Wineries", "Latitude", c => c.Double(nullable: false));
            DropColumn("dbo.Wineries", "Raiting");
            DropColumn("dbo.Wineries", "ImageUrl");
        }
    }
}
