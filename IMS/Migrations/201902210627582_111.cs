namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _111 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkBasics", "WorkContent", c => c.String(nullable: false, maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkBasics", "WorkContent", c => c.String(nullable: false));
        }
    }
}
