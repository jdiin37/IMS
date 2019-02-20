namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addWorkTrace : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraceDetails",
                c => new
                    {
                        SeqNo = c.Int(nullable: false, identity: true),
                        TraceNo = c.String(nullable: false),
                        WorkCode = c.String(),
                        WorkDate = c.DateTime(nullable: false),
                        WorkUser = c.String(),
                        Status = c.String(maxLength: 2),
                        CreDate = c.DateTime(nullable: false),
                        CreUser = c.String(),
                        ModDate = c.DateTime(),
                        ModUser = c.String(),
                    })
                .PrimaryKey(t => t.SeqNo);
            
            CreateTable(
                "dbo.TraceMasters",
                c => new
                    {
                        SeqNo = c.Int(nullable: false, identity: true),
                        TraceNo = c.String(nullable: false, maxLength: 20),
                        PigFarmId = c.String(),
                        Status = c.String(maxLength: 2),
                        CreDate = c.DateTime(nullable: false),
                        CreUser = c.String(),
                        ModDate = c.DateTime(),
                        ModUser = c.String(),
                    })
                .PrimaryKey(t => t.SeqNo)
                .Index(t => t.TraceNo, unique: true);
            
            CreateTable(
                "dbo.WorkBasics",
                c => new
                    {
                        SeqNo = c.Int(nullable: false, identity: true),
                        WorkCode = c.String(nullable: false, maxLength: 20),
                        WorkType = c.String(),
                        WorkContent = c.String(nullable: false),
                        Memo = c.String(),
                        Status = c.String(maxLength: 2),
                        CreDate = c.DateTime(nullable: false),
                        CreUser = c.String(),
                        ModDate = c.DateTime(),
                        ModUser = c.String(),
                    })
                .PrimaryKey(t => t.SeqNo)
                .Index(t => t.WorkCode, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.WorkBasics", new[] { "WorkCode" });
            DropIndex("dbo.TraceMasters", new[] { "TraceNo" });
            DropTable("dbo.WorkBasics");
            DropTable("dbo.TraceMasters");
            DropTable("dbo.TraceDetails");
        }
    }
}
