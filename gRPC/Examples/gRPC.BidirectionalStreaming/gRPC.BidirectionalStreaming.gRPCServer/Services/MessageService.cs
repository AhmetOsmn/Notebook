using gRPC.Bidi.gRPCServer;
using Grpc.Core;

namespace gRPC.BidirectionalStreaming.gRPCServer.Services
{
    public class MessageService : Message.MessageBase
    {
        public override async Task SendMessage(IAsyncStreamReader<MessageRequest> requestStream, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
        {
            var task1 = Task.Run(async () =>
            {
                while (await requestStream.MoveNext(context.CancellationToken))
                {
                    Console.WriteLine($"Client'tan servera gelen mesaj: {requestStream.Current.Name} -> {requestStream.Current.Message}");
                }
            });

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new MessageResponse { Message = "Mesaj alındı: " + i});
            }

            await task1;
        }
    }
}