namespace DIANS_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class winery3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wineries", "DefaultRating", c => c.Single(nullable: false));
            AddColumn("dbo.Wineries", "Rating", c => c.Single(nullable: false));
            DropColumn("dbo.Wineries", "Raiting");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wineries", "Raiting", c => c.Single(nullable: false));
            DropColumn("dbo.Wineries", "Rating");
            DropColumn("dbo.Wineries", "DefaultRating");
        }
    }
}
