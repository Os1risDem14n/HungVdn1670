namespace HungVdn1670.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserDetailDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        TOEICScore = c.Int(nullable: false),
                        ProgrammingLanguage = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDetails", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserDetails", new[] { "UserId" });
            DropTable("dbo.UserDetails");
        }
    }
}
