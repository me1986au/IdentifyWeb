namespace IdentifyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonsAttribute : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AddressSubAttribute", "PersonsAttributeId", "dbo.PersonsAttribute");
            DropForeignKey("dbo.PersonalSubAttributes", "PersonsAttributeId", "dbo.PersonsAttribute");
            DropForeignKey("dbo.PhoneNumberSubAttributes", "PersonsAttributeId", "dbo.PersonsAttribute");
            DropForeignKey("dbo.TimeFrameSubAttributes", "PersonsAttributeId", "dbo.PersonsAttribute");
            DropIndex("dbo.AddressSubAttribute", new[] { "PersonsAttributeId" });
            DropIndex("dbo.PersonalSubAttributes", new[] { "PersonsAttributeId" });
            DropIndex("dbo.PhoneNumberSubAttributes", new[] { "PersonsAttributeId" });
            DropIndex("dbo.TimeFrameSubAttributes", new[] { "PersonsAttributeId" });
            DropTable("dbo.AddressSubAttribute");
            DropTable("dbo.PersonalSubAttributes");
            DropTable("dbo.PhoneNumberSubAttributes");
            DropTable("dbo.TimeFrameSubAttributes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TimeFrameSubAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        PersonsAttributeId = c.Int(nullable: false),
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.TimeFrameSubAttributes", "PersonsAttributeId");
            CreateIndex("dbo.PhoneNumberSubAttributes", "PersonsAttributeId");
            CreateIndex("dbo.PersonalSubAttributes", "PersonsAttributeId");
            CreateIndex("dbo.AddressSubAttribute", "PersonsAttributeId");
            AddForeignKey("dbo.TimeFrameSubAttributes", "PersonsAttributeId", "dbo.PersonsAttribute", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PhoneNumberSubAttributes", "PersonsAttributeId", "dbo.PersonsAttribute", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PersonalSubAttributes", "PersonsAttributeId", "dbo.PersonsAttribute", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AddressSubAttribute", "PersonsAttributeId", "dbo.PersonsAttribute", "Id", cascadeDelete: true);
        }
    }
}
