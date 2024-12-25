namespace soundyard.club.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAgreementColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Agreement", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Agreement", c => c.String());
        }
    }
}
