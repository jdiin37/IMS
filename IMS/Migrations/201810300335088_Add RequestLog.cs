namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequestLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestLogs",
                c => new
                    {
                        RequestLogSN = c.Int(nullable: false, identity: true),
                        LogTime = c.DateTime(nullable: false),
                        IP = c.String(maxLength: 30),
                        Host = c.String(maxLength: 30),
                        browser = c.String(),
                        requestType = c.String(maxLength: 30),
                        userHostAddress = c.String(maxLength: 30),
                        userHostName = c.String(maxLength: 30),
                        httpMethod = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.RequestLogSN);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RequestLogs");
        }
    }
}
