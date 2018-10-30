namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AccountNo = c.String(nullable: false, maxLength: 12),
                        Password = c.String(),
                        Email = c.String(),
                        Status = c.String(),
                        CreDate = c.DateTime(nullable: false),
                        CreUser = c.String(),
                        ModDate = c.DateTime(),
                        ModUser = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.AccountNo });
            
            DropTable("dbo.PigResumes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PigResumes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PigNo = c.String(),
                        PigFarmID = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Type = c.String(),
                        Slaughterhouse = c.String(),
                        CreDate = c.DateTime(nullable: false),
                        CreUser = c.String(),
                        ModDate = c.DateTime(nullable: false),
                        ModUser = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Accounts");
        }
    }
}
