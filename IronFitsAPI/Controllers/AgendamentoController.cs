using IronFitsApi.Models;
using IronFitSApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IronFitsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AgendamentoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Agendar(Agendamento agt)
        {
            var novoagendamento = new Agendamento
            {
                DataAgendamento = agt.DataAgendamento,
                Status = agt.Status,
                ClienteId = agt.ClienteId,
                PersonalId = agt.PersonalId
            };
            _context.SaveChanges();
            await _context.SaveChangesAsync();
            return Ok(novoagendamento);
        }

        [HttpGet]
        public async Task<ActionResult<List<Agendamento>>> GetAgendamento()
        { 
            var  agendamento = await _context.Agendamentos
                .Include(a => a.DataAgendamento)
                .Select(a => new Agendamento
                {
                    ClienteId = a.ClienteId,
                    PersonalId = a.PersonalId,
                    DataAgendamento = a.DataAgendamento,
                    Status = a.Status,
                }).ToListAsync();
            return Ok(agendamento);
         }
    }
}
