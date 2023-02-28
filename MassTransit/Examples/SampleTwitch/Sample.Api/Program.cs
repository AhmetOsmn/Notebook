
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sample.Components.Consumers;
using Sample.Contracts;
using Sample.Service;

namespace Sample.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

            builder.Services.AddMassTransit(cfg =>
            {
                cfg.AddBus(provider => Bus.Factory.CreateUsingRabbitMq());

                cfg.AddRequestClient<SubmitOrder>();
            });

            builder.Services.AddHostedService<MassTransitConsoleHostedService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}