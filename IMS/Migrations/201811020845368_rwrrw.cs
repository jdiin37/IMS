namespace IMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rwrrw : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Accounts");
            AddColumn("dbo.Accounts", "SeqNo", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Accounts", "Status", c => c.String(maxLength: 2));
            AddPrimaryKey("dbo.Accounts", "SeqNo");
            DropColumn("dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Accounts");
            AlterColumn("dbo.Accounts", "Status", c => c.String());
            DropColumn("dbo.Accounts", "SeqNo");
            AddPrimaryKey("dbo.Accounts", "Id");
        }
    }
}
