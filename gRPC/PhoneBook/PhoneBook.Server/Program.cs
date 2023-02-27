using PhoneBook.Server.Repositories;
using PhoneBook.Server.Services;

namespace PhoneBook.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddSingleton<PhoneBookRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // Kullanacagimiz gRPC servisini burada register etmek zorundayiz.
            app.MapGrpcService<PhoneBookService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}