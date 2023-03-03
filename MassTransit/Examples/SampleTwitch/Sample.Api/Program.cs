using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sample.Contracts;
using Sample.Service;

namespace Sample.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Api";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

            builder.Services.AddMassTransit(cfg =>
            {
                cfg.AddBus(provider => Bus.Factory.CreateUsingRabbitMq());

                cfg.AddRequestClient<SubmitOrder>(new Uri($"exchange:{RmqConstants.QueueName}"));
            });

            builder.Services.AddHostedService<MassTransitConsoleHostedService>();

            builder.Services.AddControllers();

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