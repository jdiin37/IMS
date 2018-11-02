namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccountLevel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Level = c.String(),
                        LevelName = c.String(),
                        Status = c.String(),
                        CreDate = c.DateTime(nullable: false),
                        CreUser = c.String(),
                        ModDate = c.DateTime(),
                        ModUser = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountLevels");
        }
    }
}
