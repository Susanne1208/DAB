using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace DocumentDB
{
    public class Repository
    {
        public Program Program;
        private readonly DocumentClient _client;

        public Repository(DocumentClient client, Program program)
        {
            Program = program;
            _client = client;

        }
        public async Task CreatePerson()
        {
            AddToDatabase(InitPerson());
        }

        // Makes a Person object for later use
        private Person InitPerson()
        {
            

            Person arminaPerson = new Person
            {
                Id = "Armina.8",
                Name = "Armina",
                MiddleName = "Isabella",
                LastName = "Sanjari",
                

                Email = new Email
                    {
                        EmailAddress = "armina1506@hotmail.com",
                        EmailType = "privat"
                
                    },
                PhoneNr = new PhoneNr
                {
                    PhoneNumber = "27289764",
                    PhoneType = "privat",
                    PhoneCompany = "nej"
                },

                PrimaryAddress = new PrimaryAddress
                {
                    PrimaryAddressType = "privat",
                    CityName = "Aarhus",
                    HouseNumber = "6",
                    StreetName = "Haslevej",
                    ZipCode = "8000"
                    
                },

                AltAddress = new AltAddress
                {
                    AltAddressType = "skole",
                    CityName = "katrinebjerg",
                    HouseNumber = "46",
                    StreetName = "finderupvej",
                    ZipCode = "8200"
                },
            
            };

           
            return arminaPerson;
        }

        public async Task ReadPerson(string databaseName, string collectionName)
        {
            Console.WriteLine("Enter person ID: ");
            string personID = Console.ReadLine();

            // Set some common query options
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            // Here we find the Person via its ID
            IQueryable<Person> personQuery = _client.CreateDocumentQuery<Person>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), queryOptions)
                .Where(p => p.Id == personID);

            // Wanted person is now in PersonQuery
            foreach (Person person in personQuery)
            {
                Console.WriteLine("\tRead {0}", person);
            }
        }

        public async Task UpdatePerson()
        {
            var person1 = InitPerson();   //Create a new Person
            try
            {
                await ReplacePersonDocument(Program.DatabaseId, Program.DatabaseId, person1.Id, person1);   //Replace old person with new one
            }
            catch (Exception e)
            {
                Console.WriteLine("Person did not exist. Nothing has been updated.");   //When trying to update a person that does not exist
            }
        }

        public async Task DeletePerson()
        {
            Console.WriteLine("Write Person ID for person you want to delete from the database: ");
            string personId = Console.ReadLine();

            try
            {
                await DeletePersonDocument(Program.DatabaseId, Program.DatabaseId, personId);    //Deletes Person (document) with the wanted ID
            }
            catch (Exception e)
            {
                Console.WriteLine("Person does not exist. Nothing has been deleted");   //When Person doesn't exist
            }
        }


        //The following methods works directly with the database

        private async Task ReplacePersonDocument(string databaseName, string collectionName, string personId, Person updatedPerson)
        {
            Console.WriteLine("ReplacePersonDocument()" + personId);
            await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, personId), updatedPerson);
            Console.WriteLine("Replaced Person {0}", personId);
        }

        private async Task AddToDatabase(Person person)
        {
            await CreatePersonDocumentIfNotExists(Program.DatabaseId, Program.DatabaseId, person);
        }

        private async Task DeletePersonDocument(string databaseName, string collectionName, string documentName)
        {
            await _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, documentName));
            Console.WriteLine("Deleted Person {0}", documentName);
        }

        private async Task CreatePersonDocumentIfNotExists(string databaseName, string collectionName, Person person)
        {
            try
            {
                await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName,
                    person.Id));
                Console.WriteLine("Person {0} exists already. Nothing created.", person.Id);
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await _client.CreateDocumentAsync(
                        UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), person);
                    Console.WriteLine("Created Person {0}", person.Id);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
