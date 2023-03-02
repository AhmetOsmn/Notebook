using Grpc.Core;
using PhoneBook.Server.Repositories;

namespace PhoneBook.Server.Services
{
    public class PhoneBookService : PhoneBook.PhoneBookBase
    {
        private readonly PhoneBookRepository _phoneBookRepository;

        public PhoneBookService(PhoneBookRepository phoneBookRepository)
        {
            _phoneBookRepository = phoneBookRepository;
        }

        public override Task<ContactModel> CreateNewContact(ContactModel request, ServerCallContext context)
        {
            var response = _phoneBookRepository.AddContact(request);
            return Task.FromResult(response);
        }

        public override Task<ContactList> GetAllContact(Empty request, ServerCallContext context)
        {
            return Task.FromResult(_phoneBookRepository.GetAll());
        }

        public override async Task GetAllContactAsServerStream(Empty request, IServerStreamWriter<ContactModel> responseStream, ServerCallContext context)
        {
            //var contactList = _phoneBookRepository.GetAll();
            //foreach (var item in contactList.Contacts)
            //{
            //    await responseStream.WriteAsync(item);
            //    await Task.Delay(300);
            //}
            var contactList = _phoneBookRepository.GetAll();
            var writeTask = Task.Run(async () =>
            {
                foreach (var item in contactList.Contacts)
                {
                    await responseStream.WriteAsync(item);
                    await Task.Delay(300);
                }
            });
            await writeTask;
        }

        public override async Task<CreateResult> CreateNewContactAsClientStream(IAsyncStreamReader<ContactModel> requestStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                _phoneBookRepository.AddContact(requestStream.Current);
            }

            return await Task.FromResult(new CreateResult { Message = "Contacts added." });
        }

        public override async Task CreateNewContactAsBidiStream(IAsyncStreamReader<ContactModel> requestStream, IServerStreamWriter<CreateResult> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext().ConfigureAwait(false))
            {
                var current = requestStream.Current;
                _phoneBookRepository.AddContact(current);
                await responseStream.WriteAsync(new CreateResult { Message = $"{current.FirstName} added." }).ConfigureAwait(false);
            }
        }
    }
}
