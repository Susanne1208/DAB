using System;
using System.Linq;
using System.Threading.Tasks;

// ADD THIS PART TO YOUR CODE
using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace DocumentDB
{
    public class Program
    {
        //Connects to Azure DB account
        private const string EndpointUrl = "https://localhost:8081";

        private const string PrimaryKey =
            "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        private static DocumentClient _client;
        private static Program _program;
        private static Repository _repository;


        static void Main(string[] args)
        {
            //To run asynchronous task from your Main method. 
            try
            {
                _program = new Program();
                _program.GetStartedDemo().Wait();
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }
            finally
            {
                Console.WriteLine("End of demo, press any key to exit.");
                Console.ReadKey();
            }
        }

        //Initializes the new document client
        private async Task GetStartedDemo()
        {
            _client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
            _repository = new Repository(_client, _program);

            //Creates a database named "PersonKartotek" 
            await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = "PersonKartotek" });

            //Creates a documentcollection named "PersonCollection"
            await _client.CreateDocumentCollectionIfNotExistsAsync
                (UriFactory.CreateDatabaseUri("PersonKartotek"), new DocumentCollection { Id = "PersonCollection" });

            Person arminaPerson = new Person
            {
                Id = "AAA",
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

            await _repository.CreatePerson("PersonKartotek", "PersonCollection", arminaPerson);

            _repository.ReadPerson("PersonKartotek", "PersonCollection");

            //Update Arminas name to Armina_1
            arminaPerson.Name = "Armina_1";

            await _repository.UpdatePerson("PersonKartotek", "PersonCollection", "AAA", arminaPerson);

            _repository.ReadPerson("PersonKartotek", "PersonCollection");

            //Deletes code
            await _repository.DeletePerson("PersonKartotek", "PersonCollection", "AAA");

            // Clean up/delete the database
            await _client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri("PersonKartotek"));
        }

        public void WriteToConsoleAndPromptToContinue(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }
    }
}