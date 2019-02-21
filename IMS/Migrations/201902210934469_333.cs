namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _333 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TraceDetails", "WorkMemo", c => c.String());
            AddColumn("dbo.TraceMasters", "Remark", c => c.String());
            AddColumn("dbo.TraceMasters", "PkgDate", c => c.DateTime());
            AlterColumn("dbo.TraceDetails", "WorkCode", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TraceDetails", "WorkCode", c => c.String());
            DropColumn("dbo.TraceMasters", "PkgDate");
            DropColumn("dbo.TraceMasters", "Remark");
            DropColumn("dbo.TraceDetails", "WorkMemo");
        }
    }
}
