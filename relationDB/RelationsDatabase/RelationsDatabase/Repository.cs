using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//CRUD OPERATIONER
//Repository
//unit of work linket/forbindelse mellem repository og din database. fx createperson, så bliver den created i database
namespace RelationsDatabase
{
    class Repository
    {
        public void CreatePerson(string name, string middlename, string lastname, Email email, PhoneNr phoneNr,
            PrimaryAddress primaryaddress, AltAddress altaddress)
        {
            using (var db = new BloggingContext())
            {
                var person = new Person
                {
                    Name = name,
                    MiddleName = middlename,
                    LastName = lastname,
                    Email = email,
                    PhoneNr = phoneNr,
                    PrimaryAddress = primaryaddress,
                    AltAddress = altaddress
                };
                db.Persons.Add(person);
                db.SaveChanges();

            }
        }

        public void CreateEmail(string emailAddress, string emailType)
        {
            using (var db = new BloggingContext())
            {
                var email = new Email 
                {
                    EmailAddress = emailAddress,
                    EmailType = emailType
                };
                db.Emails.Add(email);
                db.SaveChanges();
            }
        }
        public void CreatePhoneNr(string phonenumber, string phonetype, string phonecompany)
        {
            using (var db = new BloggingContext())
            {
                var phoneNr = new PhoneNr
                {
                    PhoneNumber = phonenumber,
                    PhoneType = phonetype,
                    PhoneCompany = phonecompany
                };
                db.PhoneNrs.Add(phoneNr);
                db.SaveChanges();
            }
        }

        public void CreatePrimaryAddress(string primaryaddresstype, string streetname, string housenumber, string zipcode,
            string cityname)
        {
            using (var db = new BloggingContext())
            {
                var primaryAaddress = new PrimaryAddress
                {
                    PrimaryAddressType = primaryaddresstype,
                    StreetName = streetname,
                    HouseNumber = housenumber,
                    ZipCode = zipcode,
                    CityName =cityname
                };

                db.PrimaryAddresses.Add(primaryAaddress);
                db.SaveChanges();
            }
        }

        public void CreateAltAddress(string altadresstype, string streetname, string housenumber, string zipcode,
            string cityname)
        {
            using (var db = new BloggingContext())
            {
                var altAddress= new AltAddress
                {
                    AltAddressType = altadresstype,
                    StreetName = streetname,
                    HouseNumber = housenumber,
                    ZipCode = zipcode,
                    CityName = cityname
                };

                db.AltAddresses.Add(altAddress);
                db.SaveChanges();
            }
        }

        public void ReadPerson()
        {
            using (var db = new BloggingContext())
            {
                //Display all Persons from the database
                var query = from b in db.Persons
                    orderby b.Name
                    select b;

                Console.WriteLine("All Persons in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                    Console.WriteLine(item.MiddleName);
                    Console.WriteLine(item.LastName);
                }
            }
        }

        public void ReadEmail()
            {
                using (var db = new BloggingContext())
                {
                    //Display all Emails from the database
                    var query = from b in db.Emails
                        orderby b.EmailAddress
                        select b;

                    Console.WriteLine("All Emails in the database:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.EmailAddress);
                        Console.WriteLine(item.EmailType);
                    
                    }
                }
            }

        public void ReadPhoneNr()
        {
            using (var db = new BloggingContext())
            {
                //Display all PhoneNrs from the database
                var query = from b in db.PhoneNrs
                    orderby b.PhoneNumber
                    select b;

                Console.WriteLine("All Phonenr in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.PhoneNumber);
                    Console.WriteLine(item.PhoneType);
                    Console.WriteLine(item.PhoneCompany);

                }
            }
        }

        public void ReadPrimaryAddress()
        {
            using (var db = new BloggingContext())
            {
                //Display all Primaryaddress from the database
                var query = from b in db.PrimaryAddresses
                    orderby b.PrimaryAddressType
                    select b;

                Console.WriteLine("All primaryaddress in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.PrimaryAddressType);
                    Console.WriteLine(item.StreetName);
                    Console.WriteLine(item.HouseNumber);
                    Console.WriteLine(item.ZipCode);
                    Console.WriteLine(item.CityName);


                }
            }
        }

        public void ReadAltAddress()
        {
            using (var db = new BloggingContext())
            {
                //Display all Primaryaddress from the database
                var query = from b in db.AltAddresses
                    orderby b.AltAddressType
                    select b;

                Console.WriteLine("All altaddress in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.AltAddressType);
                    Console.WriteLine(item.StreetName);
                    Console.WriteLine(item.HouseNumber);
                    Console.WriteLine(item.ZipCode);
                    Console.WriteLine(item.CityName);
                }
            }
        }

        public void DeletePerson(string name, string lastname)
        {
            using (var db = new BloggingContext())
            {
                var personDelete =
                    from p in db.Persons
                    where p.Name == name && p.LastName == lastname
                    select p;
                foreach (var person in personDelete)
                {
                    db.Persons.Remove(person);
                    Console.WriteLine("person {0} {1} is deleted from database", person.Name, person.LastName);

                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    
                }
            }
        }

        public void DeleteEmail(string emailaddress)
        {
            using (var db = new BloggingContext())
            {
                var EmailDelete =
                    from p in db.Emails
                    where p.EmailAddress == emailaddress
                    select p;
                foreach (var email in EmailDelete)
                {
                    db.Emails.Remove(email);
                    Console.WriteLine("Email {0} is deleted from database", email.EmailAddress);

                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
            }
        }
        

        public void DeletePhoneNr(string phonenumber)
        {
            using (var db = new BloggingContext())
            {
                var phoneNrDelete =
                    from p in db.PhoneNrs
                    where p.PhoneNumber==phonenumber
                    select p;
                foreach (var phoneNr in phoneNrDelete)
                {
                    db.PhoneNrs.Remove(phoneNr);
                    Console.WriteLine("Phonenumber {0} is deleted from database", phoneNr.PhoneNumber);

                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    
                }
            }
        }

        public void DeletePrimaryAddress(string primaryaddresstype)
        {
            using (var db = new BloggingContext())
            {
                var primaryAddressDelete =
                    from p in db.PrimaryAddresses
                    where p.PrimaryAddressType==primaryaddresstype
                    select p;
                foreach (var primaryAddress in primaryAddressDelete)
                {
                    db.PrimaryAddresses.Remove(primaryAddress);
                    Console.WriteLine("primaryaddress {0} is deleted from database",primaryAddress.PrimaryAddressType );

                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
            }
        }

        public void DeleteAltAddress(string altaddresstype)
        {
            using (var db = new BloggingContext())
            {
                var altAddressDelete =
                    from p in db.AltAddresses
                    where p.AltAddressType  == altaddresstype
                    select p;
                foreach (var AltAddress in altAddressDelete)
                {
                    db.AltAddresses.Remove(AltAddress);
                    Console.WriteLine("Altaddress {0} is deleted from database", AltAddress.AltAddressType);

                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
            }
        }

        public void UpdatePerson(string deletename, string deletelastname, string name, string middlename,
            string lastname, Email email, PhoneNr phoneNr,
            PrimaryAddress primaryaddress, AltAddress altaddress)
        {
            using (var db = new BloggingContext())
            {
                var personDelete =
                    from p in db.Persons
                    where p.Name == deletename && p.LastName == deletelastname
                    select p;
                foreach (var person in personDelete)
                {
                    db.Persons.Remove(person);
                    Console.WriteLine("person {0} {1} is deleted from database", person.Name, person.LastName);

                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }

                var person1 = new Person
                {
                    Name = name,
                    MiddleName = middlename,
                    LastName = lastname,
                    Email = email,
                    PhoneNr = phoneNr,
                    PrimaryAddress = primaryaddress,
                    AltAddress = altaddress
                };
                db.Persons.Add(person1);
                db.SaveChanges();
            }
        }

        public void UpdateEmail(string deleteEmail, string emailadresse, string emailtype)
        {
            using (var db = new BloggingContext())
            {
                var EmailDelete =
                    from p in db.Emails
                    where p.EmailAddress == deleteEmail
                    select p;
                foreach (var email in EmailDelete)
                {
                    db.Emails.Remove(email);
                    Console.WriteLine("Email {0} is deleted from database", email.EmailAddress);

                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }

                var email1 = new Email
                {
                    EmailAddress = emailadresse,
                    EmailType = emailtype
                };
                db.Emails.Add(email1);
                db.SaveChanges();
            }

        }

        public void UpdatePhoneNrs(string deletephonenumber, string phonenumber, string phonetype, string phonecompany)
        {
            using (var db = new BloggingContext())
            {
                var phoneNrDelete =
                    from p in db.PhoneNrs
                    where p.PhoneNumber == deletephonenumber
                    select p;
                foreach (var phoneNr in phoneNrDelete)
                {
                    db.PhoneNrs.Remove(phoneNr);
                    Console.WriteLine("Phonenumber {0} is deleted from database", phoneNr.PhoneNumber);

                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
                var phoneNr1 = new PhoneNr
                {
                    PhoneNumber = phonenumber,
                    PhoneType = phonetype,
                    PhoneCompany = phonecompany
                };
                db.PhoneNrs.Add(phoneNr1);
                db.SaveChanges();
            }
        }

        public void UpdatePrimaryAddress(string deleteprimaryaddresstype, string primaryaddresstype, string streetname, string housenumber, string zipcode, string cityname)
        {
            using (var db = new BloggingContext())
            {
                var primaryAddressDelete =
                    from p in db.PrimaryAddresses
                    where p.PrimaryAddressType == deleteprimaryaddresstype
                    select p;
                foreach (var primaryAddress in primaryAddressDelete)
                {
                    db.PrimaryAddresses.Remove(primaryAddress);
                    Console.WriteLine("primaryaddress {0} is deleted from database", primaryAddress.PrimaryAddressType);

                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
                var primaryAaddress1 = new PrimaryAddress
                {
                    PrimaryAddressType = primaryaddresstype,
                    StreetName = streetname,
                    HouseNumber = housenumber,
                    ZipCode = zipcode,
                    CityName = cityname
                };

                db.PrimaryAddresses.Add(primaryAaddress1);
                db.SaveChanges();
            }
        }

        public void UpdateAltaddress(string deletealtaddresstype, string altaddresstype, string streetname,
            string housenumber, string zipcode, string cityname)
        {
            using (var db = new BloggingContext())
            {
                var altAddressDelete =
                    from p in db.AltAddresses
                    where p.AltAddressType == deletealtaddresstype
                    select p;
                foreach (var AltAddress in altAddressDelete)
                {
                    db.AltAddresses.Remove(AltAddress);
                    Console.WriteLine("Altaddress {0} is deleted from database", AltAddress.AltAddressType);

                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
                var altAaddress1 = new AltAddress
                {
                    AltAddressType =altaddresstype,
                    StreetName = streetname,
                    HouseNumber = housenumber,
                    ZipCode = zipcode,
                    CityName = cityname
                };

                db.AltAddresses.Add(altAaddress1);
                db.SaveChanges();
            }
        }

    }
}

