using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RelationsDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            //myFunctions.AddEmail("armina1506@hotmail.com", "privat");
            //myFunctions.AddPhoneNr("27289764","privat","No phoneCompany");
            //myFunctions.AddPrimaryAddress("Home","Haslevej","6","8230","aabyhoej");
            //myFunctions.AddAltAdress("Second Home","Finlandsgade","22","8200","Aarhus Nord");

            Email email = new Email("armina1506@hotmail.com", "privat");
            PhoneNr phoneNr = new PhoneNr("27289764", "privat", "No phoneCompany");
            PrimaryAddress primaryAddress = new PrimaryAddress("Home", "Haslevej", "6", "8230", "aabyhoej");
            AltAddress altAddress = new AltAddress("Second Home", "Finlandsgade", "22", "8200", "Aarhus Nord");

            Email email1 = new Email("Emma@hotmail.com", "privat");
            PhoneNr phoneNr1 = new PhoneNr("123456", "privat", "No phoneCompany");
            PrimaryAddress primaryAddress1 = new PrimaryAddress("Emma_Home", "Haslevej", "6", "8230", "aabyhoej");
            AltAddress altAddress1 = new AltAddress("Emma_Second Home", "Finlandsgade", "22", "8200", "Aarhus Nord");

            Email email2 = new Email("Susanne@hotmail.com", "privat");
            PhoneNr phoneNr2 = new PhoneNr("26498726", "privat", "No phoneCompany");
            PrimaryAddress primaryAddress2 = new PrimaryAddress("Susanne_Home", "Haslevej", "6", "8230", "aabyhoej");
            AltAddress altAddress2 = new AltAddress("Susanne_Second Home", "Finlandsgade", "22", "8200", "Aarhus Nord");

            //CREATE OPERATION
            repository.CreatePerson("Arminaa", "Isabellaa", "Sanjarii", email, phoneNr, primaryAddress, altAddress);
            //repository.CreatePerson("Emma", "mems", "rolsted", email1, phoneNr1, primaryAddress1, altAddress1);
            //repository.CreatePerson("Sus","ema","armina", email, phoneNr, primaryAddress, altAddress);
            //repository.CreatePerson("mems","isa","susi", email, phoneNr, primaryAddress, altAddress);

            //READ OPERATION
            //repository.ReadPerson();
            //repository.ReadEmail();
            //repository.ReadPhoneNr();
            //repository.ReadPrimaryAddress();
            //repository.ReadAltAddress();

            //DELETE OPERATION
            //repository.DeletePerson("Emma","rolsted");
            //repository.DeleteEmail("armina1506@hotmail.com");
            //repository.DeleteEmail();
            //repository.DeletePhoneNr();
            //repository.DeletePrimaryAddress();
            //repository.DeleteAltAddress();

            //Update operation
            //repository.UpdatePerson("Armina","Sanjari","Susanne","Sus","Olsen",email2,phoneNr2,primaryAddress2,altAddress2);
        }

    }

    //public class Kartotek
    //{
    //    [Key]
    //    public string Name { get; set; }
    //    public virtual List<Person> Persons { get; set; }
    // Skal add-migration Kartotek
    // Og opdater Updata-Database ??!
    //}

    public class Person
    {
        [Key]
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        
        public string EmailAddress { get; set; } // Primary key i Email klassen, men den bliver foreing key her.
        public virtual Email Email { get; set; } //foreing key

        public string PhoneNumber { get; set; }
        public virtual PhoneNr PhoneNr { get; set; }

        public string PrimaryAddressType { get; set; }
        public PrimaryAddress PrimaryAddress { get; set; }

        public string AltAddressType { get; set; }
        public AltAddress AltAddress { get; set; }
    }

    public class BloggingContext : DbContext  //contekst ned til din database. Nu fortæller jeg 
    {
        public BloggingContext() : base("name=HandIn2-2")
        {
        }
        
        public DbSet<Person> Persons { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet <PhoneNr> PhoneNrs { get; set; }
        public DbSet<PrimaryAddress> PrimaryAddresses { get; set; }

        public DbSet<AltAddress> AltAddresses { get; set; }
    }

    public class Email
    {
        public Email()
        {} 
        public Email(string emailAddress, string emailType)
        {
            EmailAddress = emailAddress;
            EmailType = emailType;
        }

        [Key] //specificer den efterfølgende funktion som primarykey
        public string EmailAddress { get; set; }
        public string EmailType { get; set; }
    }

    public class PhoneNr
    {
        public PhoneNr() { }
        public PhoneNr(string phonenumber, string phonetype, string phonecompany)
        {
            PhoneNumber = phonenumber;
            PhoneType = phonetype;
            PhoneCompany = PhoneCompany;
        }

        [Key]

        public string PhoneNumber { get; set; }
        public string PhoneType { get; set; }
        public string PhoneCompany { get; set; }
    }

    public class PrimaryAddress
    {
        public PrimaryAddress()
        {
        }

        public PrimaryAddress(string primaryaddresstype, string streetname, string housenumber, string zipcode,
            string cityname)
        {
            PrimaryAddressType = primaryaddresstype;
            StreetName = streetname;
            HouseNumber = housenumber;
            ZipCode = zipcode;
            CityName = cityname;
        }
        [Key]
        public string PrimaryAddressType { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
        public string CityName { get; set; }
    }

    public class AltAddress
    {
        public AltAddress()
        { }

        public AltAddress(string altadresstype, string streetname, string housenumber, string zipcode,
            string cityname)
        {
            AltAddressType = altadresstype;
            StreetName = streetname;
            HouseNumber = housenumber;
            ZipCode = zipcode;
            CityName = cityname;
        }
        [Key]
        public string AltAddressType { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
        public string CityName { get; set; }
    }



}
