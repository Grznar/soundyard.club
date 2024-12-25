namespace soundyard.club.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAgreementToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Agreement", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Agreement");
        }
    }
}
