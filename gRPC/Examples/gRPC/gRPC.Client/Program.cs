using Grpc.Net.Client;

namespace gRPC.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7077");
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(new HelloRequest { Name = "Ahmet"});
            var getDateResult = await client.GetDateAsync(new Empty());
            var getStudentListResult = await client.GetStudentsAsync(new Empty());

            Console.WriteLine("Greeter reply message: " + reply.Message);
            Console.WriteLine("Get date result: " + getDateResult.Message);
            Console.WriteLine("Students: ");
            for (int i = 0; i < getStudentListResult.Students.Count; i++)
            {
                Console.WriteLine($"{i+1}. Name: {getStudentListResult.Students[i].Name} - Age: {getStudentListResult.Students[i].Age}");
            }
            Console.ReadLine();
        }
    }
}