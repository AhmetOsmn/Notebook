using Grpc.Core;
using Grpc.Net.Client;

namespace PhoneBook.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7270");
            var client = new PhoneBook.PhoneBookClient(channel);

            #region Basic Req
            var defaultContacts = await client.GetAllContactAsync(new Empty());
            for (int i = 0; i < defaultContacts.Contacts.Count; i++)
            {
                Console.WriteLine($"{defaultContacts.Contacts[i].Id} - {defaultContacts.Contacts[i].FirstName} - {defaultContacts.Contacts[i].LastName}");
            }
            Console.WriteLine("---------------------------------------------------");
            #endregion

            #region Basic Req
            var addedContact = await client.CreateNewContactAsync(new ContactModel { FirstName = "Ahmet", LastName = "Sezgin" });
            Console.WriteLine($"Added contact: Id: {addedContact.Id} - Name: {addedContact.FirstName} - LastName: {addedContact.LastName}");
            Console.WriteLine("---------------------------------------------------");
            #endregion

            #region Streaming Server
            Console.WriteLine("################# Server Stream #################");
            using var serverStream = client.GetAllContactAsServerStream(new Empty());
            Console.WriteLine("Printing contacts..");
            while (await serverStream.ResponseStream.MoveNext())
            {
                var currentContact = serverStream.ResponseStream.Current;
                Console.WriteLine($"Server response: {currentContact.Id} - {currentContact.FirstName} - {currentContact.LastName}");
            }
            Console.WriteLine("---------------------------------------------------");
            #endregion

            #region Streaming Client
            Console.WriteLine("################# Client Stream #################");
            var contactListForAdd = new List<ContactModel>
            {
                new ContactModel { FirstName = "Osman", LastName = "Sezgin" },
                new ContactModel { FirstName = "Ali", LastName = "Kuş" },
                new ContactModel { FirstName = "Ayşe", LastName = "Demir" },
                new ContactModel { FirstName = "Mert", LastName = "Altın" },
                new ContactModel { FirstName = "Semih", LastName = "Ulu" },
                new ContactModel { FirstName = "Mehmet", LastName = "Dinç" }
            };

            var clientStream = client.CreateNewContactAsClientStream();
            foreach (var item in contactListForAdd)
            {
                await clientStream.RequestStream.WriteAsync(item);
                Console.WriteLine($"{item.FirstName} submitted.");
                await Task.Delay(300);
            }
            await clientStream.RequestStream.CompleteAsync();
            var response = await clientStream.ResponseAsync;
            Console.WriteLine($"Server response: {response.Message}");
            Console.WriteLine("---------------------------------------------------");
            #endregion

            #region Streaming Bidi
            Console.WriteLine("################# Bidi Stream #################");
            var contactsForBidiStream = new List<ContactModel>
              {
                  new ContactModel { FirstName = "test 1", LastName = "test 1" },
                  new ContactModel { FirstName = "test 2", LastName = "test 2" },
                  new ContactModel { FirstName = "test 3", LastName = "test 3" },
                  new ContactModel { FirstName = "test 4", LastName = "test 4" },
                  new ContactModel { FirstName = "test 5", LastName = "test 5" },
                  new ContactModel { FirstName = "test 6", LastName = "test 6" }
              };

            var bidiStream = client.CreateNewContactAsBidiStream();
            foreach (var item in contactsForBidiStream)
            {
                await bidiStream.RequestStream.WriteAsync(item);
                Console.WriteLine($"{item.FirstName} submitted.");

                Task.Run(async () =>
                {
                    await foreach (var response in bidiStream.ResponseStream.ReadAllAsync())
                    {
                        Console.WriteLine($"Server response: {response.Message}");
                    }
                });

                await Task.Delay(300);
            }
            Console.WriteLine("---------------------------------------------------");
            #endregion

            #region Streaming Server
            Console.WriteLine("################# Server Stream #################");
            using var serverStreamSecond = client.GetAllContactAsServerStream(new Empty());
            Console.WriteLine("Printing contacts..");
            while (await serverStreamSecond.ResponseStream.MoveNext())
            {
                var currentContact = serverStreamSecond.ResponseStream.Current;
                Console.WriteLine($"Server response: {currentContact.Id} - {currentContact.FirstName} - {currentContact.LastName}");
            }
            Console.WriteLine("---------------------------------------------------");
            #endregion

            Console.ReadLine();
        }
    }
}