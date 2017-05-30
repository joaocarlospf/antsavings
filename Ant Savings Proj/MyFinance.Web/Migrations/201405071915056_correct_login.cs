namespace MyFinance.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correct_login : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "BirthDate", c => c.Int());
        }
    }
}
