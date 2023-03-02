using gRPC.Bidi.gRPCClient;
using Grpc.Core;
using Grpc.Net.Client;

namespace gRPC.BidirectionalStreaming.gRPCClient
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7061");
            var messageClient = new Message.MessageClient(channel);

            var request = messageClient.SendMessage();

            var task1 = Task.Run(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(1000);
                    await request.RequestStream.WriteAsync(new MessageRequest { Name = "Ahmet" +i, Message = "Merhaba " + i});
                }

                await request.RequestStream.CompleteAsync();
            });

            CancellationTokenSource cancellationTokenSource = new();

            while (await request.ResponseStream.MoveNext(cancellationTokenSource.Token))
            {
                Console.WriteLine($"Server'dan client'a gelen mesaj: {request.ResponseStream.Current.Message}");
            }

            await task1;
        }
    }
}