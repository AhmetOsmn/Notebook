using gRPC.ClientSteaming.gRPCServer;
using Grpc.Core;

namespace gRPC.ClientStreaming.gRPCServer.Services
{
    public class MessageService : Message.MessageBase
    {
        public override async Task<MessageResponse> SendMessage(IAsyncStreamReader<MessageRequest> requestStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext(context.CancellationToken))
            {
                Console.WriteLine($"{requestStream.Current.Name}: {requestStream.Current.Message}");
            }

            return new MessageResponse { Message = "Servis verileri işledi" };
        }
    }
}