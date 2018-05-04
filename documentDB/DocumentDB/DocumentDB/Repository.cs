using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace DocumentDB
{
    class Repository
    {
        private Program _program;

        private DocumentClient _client;

        public Repository(DocumentClient client, Program program)
        {
            _client = client;
            _program = program;
        }

        // ADD THIS PART TO YOUR CODE
        public async Task CreatePerson(string databaseName, string collectionName, Person person)
        {
            try
            {
                await this._client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, person.Id));
                this._program.WriteToConsoleAndPromptToContinue("Found {0}", person.Id);
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await this._client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), person);
                    this._program.WriteToConsoleAndPromptToContinue("Created Person {0}", person.Id);
                }
                else
                {
                    throw;
                }
            }
        }

        public void ReadPerson(string databaseName, string collectionName)
        {
            // Set some common query options
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            // Here we find the Andersen family via its LastName
            IQueryable<Person> personQuery = this._client.CreateDocumentQuery<Person>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), queryOptions)
                .Where(f => f.LastName == "Sanjari");

            // The query is executed synchronously here, but can also be executed asynchronously via the IDocumentQuery<T> interface
            Console.WriteLine("Running LINQ query...");
            foreach (Person person in personQuery)
            {
                Console.WriteLine("\tRead {0}", person);
            }

            // Now execute the same query via direct SQL
            IQueryable<Person> personQueryInSql = this._client.CreateDocumentQuery<Person>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                "SELECT * FROM Person WHERE Person.LastName = 'Sanjari'",
                queryOptions);

            Console.WriteLine("Running direct SQL query...");
            foreach (Person person in personQueryInSql)
            {
                Console.WriteLine("\tRead {0}", person);
            }

            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();

             
        }

        public async Task UpdatePerson(string databaseName, string collectionName, string personName, Person updatedPerson)
        {
            await this._client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, personName), updatedPerson);
            this._program.WriteToConsoleAndPromptToContinue("Replaced Person {0}", personName);
        }

        public async Task DeletePerson(string databaseName, string collectionName, string documentName)
        {
            await this._client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, documentName));
            Console.WriteLine("Deleted Person {0}", documentName);
        }

    }
}
