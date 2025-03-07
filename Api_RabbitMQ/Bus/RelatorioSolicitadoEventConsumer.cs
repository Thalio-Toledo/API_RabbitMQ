using Api_RabbitMQ.Relatorios;
using MassTransit;

namespace Api_RabbitMQ.Bus
{
    public class RelatorioSolicitadoEventConsumer : IConsumer<RelatorioSolicitadoEvent>
    {
        private readonly ILogger<RelatorioSolicitadoEventConsumer> _logger;

        public RelatorioSolicitadoEventConsumer(ILogger<RelatorioSolicitadoEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RelatorioSolicitadoEvent> context)
        {
            _logger.LogInformation("Processando Relatório Id:{Id}, Nome:{Nome}", context.Message.id, context.Message.name);

            await Task.Delay(1000);

            var relatorio = Lista.Relatorios.FirstOrDefault(r => r.Id == context.Message.id);
            if (relatorio is not null)
            {
                relatorio.Status = "Completado";
                relatorio.ProcessedTime = DateTime.Now;
            }

            _logger.LogInformation("Relatório Processado Id:{Id}, Nome:{Nome}", context.Message.id, context.Message.name);
        }
    }
}
