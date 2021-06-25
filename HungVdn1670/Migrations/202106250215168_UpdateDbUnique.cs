namespace HungVdn1670.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbUnique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Categories", "Name", unique: true);
            CreateIndex("dbo.Courses", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Courses", new[] { "Name" });
            DropIndex("dbo.Categories", new[] { "Name" });
        }
    }
}
