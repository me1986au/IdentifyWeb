namespace IdentifyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressSubAttribute : DbMigration
    {
        public override void Up()
        {
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
            DropForeignKey("dbo.PersonalSubAttributes", "PersonsAttributeId", "dbo.PersonsAttribute");
            DropForeignKey("dbo.AddressSubAttribute", "PersonsAttributeId", "dbo.PersonsAttribute");
            DropIndex("dbo.TimeFrameSubAttributes", new[] { "PersonsAttributeId" });
            DropIndex("dbo.PhoneNumberSubAttributes", new[] { "PersonsAttributeId" });
            DropIndex("dbo.PersonalSubAttributes", new[] { "PersonsAttributeId" });
            DropIndex("dbo.AddressSubAttribute", new[] { "PersonsAttributeId" });
            DropTable("dbo.TimeFrameSubAttributes");
            DropTable("dbo.PhoneNumberSubAttributes");
            DropTable("dbo.PersonalSubAttributes");
            DropTable("dbo.AddressSubAttribute");
        }
    }
}
