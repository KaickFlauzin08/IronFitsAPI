using IronFitsApi.Models;
using IronFitSApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IronFitsAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
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
            _context.Agendamentos.Add(agt);
            await _context.SaveChangesAsync();
            return Ok(agt);
        }

        [HttpGet]
        public async Task<ActionResult<List<Agendamento>>> GetAgendamento()
        {
            var agendamentos = await _context.Agendamentos.ToListAsync();
            return Ok(agendamentos);
        }

        [HttpPut]
        public async Task<ActionResult> Atualizar(Agendamento agt)
        {
            _context.Agendamentos.Update(agt);
            await _context.SaveChangesAsync();
            return Ok(agt);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(int id)
        {
            var agt = await _context.Agendamentos.FindAsync(id);
            _context.Agendamentos.Remove(agt!);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}