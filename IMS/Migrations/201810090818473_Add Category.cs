namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryLists",
                c => new
                    {
                        CategoryID = c.String(nullable: false, maxLength: 10),
                        CategoryName = c.String(),
                        CreDate = c.DateTime(nullable: false),
                        CreUser = c.String(),
                        ModDate = c.DateTime(),
                        ModUser = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.CategorySubs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SubValue = c.String(),
                        SubName = c.String(),
                        CreDate = c.DateTime(nullable: false),
                        CreUser = c.String(),
                        ModDate = c.DateTime(),
                        ModUser = c.String(),
                        CategoryID = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryLists", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategorySubs", "CategoryID", "dbo.CategoryLists");
            DropIndex("dbo.CategorySubs", new[] { "CategoryID" });
            DropTable("dbo.CategorySubs");
            DropTable("dbo.CategoryLists");
        }
    }
}
