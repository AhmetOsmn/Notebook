using gRPC.ClientSteaming.gRPCClient;
using Grpc.Net.Client;

namespace gRPC.ClientStreaming.gRPCClient
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7100");
            var messageClient = new Message.MessageClient(channel);

            var request = messageClient.SendMessage();
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await request.RequestStream.WriteAsync(new MessageRequest { Name = "ahmet " + i, Message = "merhaba " + i});
            }

            await request.RequestStream.CompleteAsync();

            Console.WriteLine($"server'den gelen response: {(await request.ResponseAsync).Message}");

            Console.ReadLine();
        }
    }
}