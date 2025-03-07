using Api_RabbitMQ.Relatorios;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Api_RabbitMQ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly IBus _bus;

        public RelatorioController(IBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(Lista.Relatorios);
        
        [HttpPost("solicitar-relatorio/{name}")]
        public async Task<IActionResult> SolicitarRelatorio(string name)
        {
            var solicitacao = new SolicitacaoRelatorio()
            {
                Id = Guid.NewGuid(),
                Nome = name,
                Status = "Pendente",
                ProcessedTime = null,
            };

            Lista.Relatorios.Add(solicitacao);

            var eventRequest = new RelatorioSolicitadoEvent(solicitacao.Id, solicitacao.Nome);

            await _bus.Publish(eventRequest);

            return Ok(solicitacao);
        }
    }
}
