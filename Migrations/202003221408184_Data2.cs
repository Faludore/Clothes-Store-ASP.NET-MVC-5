namespace StoreIdent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Data2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "DnTBD", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Number", c => c.String());
            AddColumn("dbo.AspNetUsers", "DnTReg", c => c.String());
            AddColumn("dbo.AspNetUsers", "DnTDel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DnTDel");
            DropColumn("dbo.AspNetUsers", "DnTReg");
            DropColumn("dbo.AspNetUsers", "Number");
            DropColumn("dbo.AspNetUsers", "DnTBD");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
