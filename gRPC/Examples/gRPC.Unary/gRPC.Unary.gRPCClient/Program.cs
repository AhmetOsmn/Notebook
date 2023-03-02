using Grpc.Net.Client;

namespace gRPC.Unary.gRPCClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Client";

            var channel = GrpcChannel.ForAddress("https://localhost:7010");
            var messageClient = new Message.MessageClient(channel);

            var response = await messageClient.SendMessageAsync(new MessageRequest
            {
                Name = "Ahmet",
                Message = "Merhaba"
            });

            Console.WriteLine($"Döndürülen yanıt: {response.Message}");

            Console.ReadLine();
        }
    }
}