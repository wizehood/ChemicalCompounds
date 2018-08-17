namespace Junior.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBoilingTemperature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Elements", "BoilingTemperatureK", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Elements", "BoilingTemperatureK");
        }
    }
}
