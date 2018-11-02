namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyAccount : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Accounts");
            AlterColumn("dbo.Accounts", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Accounts", "Id");
            CreateIndex("dbo.Accounts", "AccountNo", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Accounts", new[] { "AccountNo" });
            DropPrimaryKey("dbo.Accounts");
            AlterColumn("dbo.Accounts", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Accounts", new[] { "Id", "AccountNo" });
        }
    }
}
