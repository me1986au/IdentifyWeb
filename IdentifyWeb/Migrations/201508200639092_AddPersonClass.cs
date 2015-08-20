namespace IdentifyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Alias = c.String(),
                        Gender = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Person", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Person", new[] { "ApplicationUserId" });
            DropTable("dbo.Person");
        }
    }
}
