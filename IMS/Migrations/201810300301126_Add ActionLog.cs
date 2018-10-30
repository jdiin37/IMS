namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActionLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionLogs",
                c => new
                    {
                        ActionLogSN = c.Int(nullable: false, identity: true),
                        LogTime = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 12),
                        AreaName = c.String(),
                        ControlName = c.String(),
                        ActionName = c.String(),
                        Parame = c.String(),
                    })
                .PrimaryKey(t => t.ActionLogSN);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ActionLogs");
        }
    }
}
