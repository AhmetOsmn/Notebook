using Grpc.Net.Client;
using gRPC.Unary.gRPCClient;

namespace gRPC.ServerStreaming.gRPCClient
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5002");
            var messageClient = new Message.MessageClient(channel);

            var response = messageClient.SendMessage(new MessageRequest
            {
                Name = "Ahmet",
                Message = "Server streaming başladı:"
            });

            CancellationTokenSource cancellationTokenSource = new();

            while (await response.ResponseStream.MoveNext(cancellationTokenSource.Token))
            {
                Console.WriteLine("Yanıt: " + response.ResponseStream.Current.Message);
            }

            Console.ReadLine();
        }
    }
}