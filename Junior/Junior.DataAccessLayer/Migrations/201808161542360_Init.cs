namespace Junior.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompoundElements",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CompoundId = c.Guid(nullable: false),
                        ElementId = c.Guid(nullable: false),
                        ElementQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Compounds", t => t.CompoundId, cascadeDelete: true)
                .ForeignKey("dbo.Elements", t => t.ElementId, cascadeDelete: true)
                .Index(t => t.CompoundId)
                .Index(t => t.ElementId);
            
            CreateTable(
                "dbo.Compounds",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TypeId = c.Guid(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CompoundTypes", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.CompoundTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Elements",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompoundElements", "ElementId", "dbo.Elements");
            DropForeignKey("dbo.CompoundElements", "CompoundId", "dbo.Compounds");
            DropForeignKey("dbo.Compounds", "TypeId", "dbo.CompoundTypes");
            DropIndex("dbo.Compounds", new[] { "TypeId" });
            DropIndex("dbo.CompoundElements", new[] { "ElementId" });
            DropIndex("dbo.CompoundElements", new[] { "CompoundId" });
            DropTable("dbo.Elements");
            DropTable("dbo.CompoundTypes");
            DropTable("dbo.Compounds");
            DropTable("dbo.CompoundElements");
        }
    }
}
