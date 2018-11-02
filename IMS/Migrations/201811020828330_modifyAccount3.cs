namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyAccount3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccountLevels", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccountLevels", "Status", c => c.String(nullable: false));
        }
    }
}
