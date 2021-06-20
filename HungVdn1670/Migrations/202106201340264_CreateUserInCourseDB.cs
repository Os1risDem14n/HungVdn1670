namespace HungVdn1670.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserInCourseDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInCourses",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CourseId, t.UserId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInCourses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserInCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.UserInCourses", new[] { "UserId" });
            DropIndex("dbo.UserInCourses", new[] { "CourseId" });
            DropTable("dbo.UserInCourses");
        }
    }
}
