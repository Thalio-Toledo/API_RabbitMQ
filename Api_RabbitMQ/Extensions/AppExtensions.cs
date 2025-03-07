using Api_RabbitMQ.Bus;
using MassTransit;

namespace Api_RabbitMQ.Extensions
{
    public static class AppExtensions
    {
        public static void AddRabbitMQService(this IServiceCollection services) 
        {
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri("amqp://localhost:5672"),host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    cfg.ConfigureEndpoints(ctx);
                });

                busConfigurator.AddConsumer<RelatorioSolicitadoEventConsumer>();
            });
        }
    }
}
