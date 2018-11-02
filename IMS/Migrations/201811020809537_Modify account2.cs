namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifyaccount2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Accounts", new[] { "AccountNo" });
            AlterColumn("dbo.Accounts", "AccountNo", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Accounts", "Password", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.Accounts", "AccountNo", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Accounts", new[] { "AccountNo" });
            AlterColumn("dbo.Accounts", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "AccountNo", c => c.String(nullable: false, maxLength: 12));
            CreateIndex("dbo.Accounts", "AccountNo", unique: true);
        }
    }
}
