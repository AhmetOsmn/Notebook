using Grpc.Core;

namespace gRPC.Unary.gRPCServer.Services
{
    public class MessageService : Message.MessageBase
    {
        public async override Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
        {
            Console.Title = "Server";
            Console.WriteLine($"{request.Name}: {request.Message}");

            return new MessageResponse { Message = "Mesaj alındı" };
        }
    }
}
