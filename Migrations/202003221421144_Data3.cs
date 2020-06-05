namespace StoreIdent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Data3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.AspNetUsers", "Number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Number", c => c.String());
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
        }
    }
}
