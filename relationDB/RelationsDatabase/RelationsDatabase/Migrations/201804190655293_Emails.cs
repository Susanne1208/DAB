namespace RelationsDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Emails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AltAddresses",
                c => new
                    {
                        AltAddressType = c.String(nullable: false, maxLength: 128),
                        StreetName = c.String(),
                        HouseNumber = c.String(),
                        ZipCode = c.String(),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.AltAddressType);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        EmailAddress = c.String(nullable: false, maxLength: 128),
                        EmailType = c.String(),
                    })
                .PrimaryKey(t => t.EmailAddress);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(maxLength: 128),
                        PhoneNumber = c.String(maxLength: 128),
                        PrimaryAddressType = c.String(maxLength: 128),
                        AltAddressType = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.AltAddresses", t => t.AltAddressType)
                .ForeignKey("dbo.Emails", t => t.EmailAddress)
                .ForeignKey("dbo.PhoneNrs", t => t.PhoneNumber)
                .ForeignKey("dbo.PrimaryAddresses", t => t.PrimaryAddressType)
                .Index(t => t.EmailAddress)
                .Index(t => t.PhoneNumber)
                .Index(t => t.PrimaryAddressType)
                .Index(t => t.AltAddressType);
            
            CreateTable(
                "dbo.PhoneNrs",
                c => new
                    {
                        PhoneNumber = c.String(nullable: false, maxLength: 128),
                        PhoneType = c.String(),
                        PhoneCompany = c.String(),
                    })
                .PrimaryKey(t => t.PhoneNumber);
            
            CreateTable(
                "dbo.PrimaryAddresses",
                c => new
                    {
                        PrimaryAddressType = c.String(nullable: false, maxLength: 128),
                        StreetName = c.String(),
                        HouseNumber = c.String(),
                        ZipCode = c.String(),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.PrimaryAddressType);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "PrimaryAddressType", "dbo.PrimaryAddresses");
            DropForeignKey("dbo.People", "PhoneNumber", "dbo.PhoneNrs");
            DropForeignKey("dbo.People", "EmailAddress", "dbo.Emails");
            DropForeignKey("dbo.People", "AltAddressType", "dbo.AltAddresses");
            DropIndex("dbo.People", new[] { "AltAddressType" });
            DropIndex("dbo.People", new[] { "PrimaryAddressType" });
            DropIndex("dbo.People", new[] { "PhoneNumber" });
            DropIndex("dbo.People", new[] { "EmailAddress" });
            DropTable("dbo.PrimaryAddresses");
            DropTable("dbo.PhoneNrs");
            DropTable("dbo.People");
            DropTable("dbo.Emails");
            DropTable("dbo.AltAddresses");
        }
    }
}
