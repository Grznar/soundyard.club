﻿namespace soundyard.club.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPasswordColumn : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Password");
        }
    }
}