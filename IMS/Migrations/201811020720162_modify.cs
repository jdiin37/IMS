namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "Level", c => c.String(nullable: false));
            AlterColumn("dbo.AccountLevels", "Level", c => c.String(nullable: false));
            AlterColumn("dbo.AccountLevels", "LevelName", c => c.String(nullable: false));
            AlterColumn("dbo.AccountLevels", "Status", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccountLevels", "Status", c => c.String());
            AlterColumn("dbo.AccountLevels", "LevelName", c => c.String());
            AlterColumn("dbo.AccountLevels", "Level", c => c.String());
            AlterColumn("dbo.Accounts", "Level", c => c.String());
            AlterColumn("dbo.Accounts", "Email", c => c.String());
            AlterColumn("dbo.Accounts", "Password", c => c.String());
        }
    }
}
