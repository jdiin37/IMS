namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _222 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkBasics", "WorkClass", c => c.String(maxLength: 20));
            AlterColumn("dbo.WorkBasics", "WorkType", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkBasics", "WorkType", c => c.String());
            DropColumn("dbo.WorkBasics", "WorkClass");
        }
    }
}
