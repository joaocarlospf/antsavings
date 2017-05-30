namespace MyFinance.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAbbreviations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Funds", "NameAbbreviation", c => c.String(maxLength: 5));
            AddColumn("dbo.Reserves", "NameAbbreviation", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reserves", "NameAbbreviation");
            DropColumn("dbo.Funds", "NameAbbreviation");
        }
    }
}
