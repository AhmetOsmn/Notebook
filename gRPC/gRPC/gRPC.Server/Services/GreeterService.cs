using Grpc.Core;

namespace gRPC.Server.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override Task<Date> GetDate(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new Date
            {
                Message = DateTime.Now.ToShortDateString()
            });
        }

        public override Task<StudentList> GetStudents(Empty request, ServerCallContext context)
        {
            var studentList = new StudentList();
            studentList.Students.Add(new Student { Name = "Ahmet", Age = 22});
            studentList.Students.Add(new Student { Name = "Osman", Age = 23 });
            return Task.FromResult(studentList);
        }
    }
}