namespace IdentifyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonsAttributeCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonsAttribute",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PersonsAttributeCategoryId = c.Int(nullable: false),
                        PersonId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .ForeignKey("dbo.PersonsAttributeCategory", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.AddressSubAttribute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetAddress = c.String(),
                        StreetAddress1 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostCode = c.String(),
                        CountryRegion = c.String(),
                        PersonsAttributeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonsAttribute", t => t.PersonsAttributeId, cascadeDelete: true)
                .Index(t => t.PersonsAttributeId);
            
            CreateTable(
                "dbo.PersonalSubAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Alias = c.String(),
                        PersonsAttributeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonsAttribute", t => t.PersonsAttributeId, cascadeDelete: true)
                .Index(t => t.PersonsAttributeId);
            
            CreateTable(
                "dbo.PersonsAttributeCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhoneNumberSubAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ext = c.String(),
                        Number = c.String(),
                        PersonsAttributeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonsAttribute", t => t.PersonsAttributeId, cascadeDelete: true)
                .Index(t => t.PersonsAttributeId);
            
            CreateTable(
                "dbo.TimeFrameSubAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        PersonsAttributeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonsAttribute", t => t.PersonsAttributeId, cascadeDelete: true)
                .Index(t => t.PersonsAttributeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeFrameSubAttributes", "PersonsAttributeId", "dbo.PersonsAttribute");
            DropForeignKey("dbo.PhoneNumberSubAttributes", "PersonsAttributeId", "dbo.PersonsAttribute");
            DropForeignKey("dbo.PersonsAttribute", "Id", "dbo.PersonsAttributeCategory");
            DropForeignKey("dbo.PersonalSubAttributes", "PersonsAttributeId", "dbo.PersonsAttribute");
            DropForeignKey("dbo.PersonsAttribute", "PersonId", "dbo.Person");
            DropForeignKey("dbo.AddressSubAttribute", "PersonsAttributeId", "dbo.PersonsAttribute");
            DropIndex("dbo.TimeFrameSubAttributes", new[] { "PersonsAttributeId" });
            DropIndex("dbo.PhoneNumberSubAttributes", new[] { "PersonsAttributeId" });
            DropIndex("dbo.PersonalSubAttributes", new[] { "PersonsAttributeId" });
            DropIndex("dbo.AddressSubAttribute", new[] { "PersonsAttributeId" });
            DropIndex("dbo.PersonsAttribute", new[] { "PersonId" });
            DropIndex("dbo.PersonsAttribute", new[] { "Id" });
            DropTable("dbo.TimeFrameSubAttributes");
            DropTable("dbo.PhoneNumberSubAttributes");
            DropTable("dbo.PersonsAttributeCategory");
            DropTable("dbo.PersonalSubAttributes");
            DropTable("dbo.AddressSubAttribute");
            DropTable("dbo.PersonsAttribute");
        }
    }
}