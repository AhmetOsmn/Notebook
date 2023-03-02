using Grpc.Core;

namespace gRPC.Unary.gRPCServer.Services
{
    public class MessageService : Message.MessageBase
    {
        public override async Task SendMessage(MessageRequest request, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine($"{request.Name}: {request.Message}");

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new MessageResponse { Message = "Merhaba" + i });
            }
        }
    }
}
