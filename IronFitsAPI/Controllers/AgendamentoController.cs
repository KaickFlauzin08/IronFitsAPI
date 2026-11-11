using IronFitsApi.Models;
using IronFitSApi.Data;
using Microsoft.AspNetCore.Authorization;
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
            // Valida se o Aluno existe no banco
            var alunoExiste = await _context.Alunos.AnyAsync(a => a.Id == agt.AlunoId);
            if (!alunoExiste)
                return BadRequest($"Aluno com ID {agt.AlunoId} não foi encontrado.");

            // Valida se o Personal existe no banco
            var personalExiste = await _context.Personais.AnyAsync(p => p.Id == agt.PersonalId);
            if (!personalExiste)
                return BadRequest($"Personal com ID {agt.PersonalId} não foi encontrado.");

            var novoAgendamento = new Agendamento
            {
                DataAgendamento = agt.DataAgendamento,
                Servico = agt.Servico,
                AlunoId = agt.AlunoId,
                PersonalId = agt.PersonalId
            };

            _context.Agendamentos.Add(novoAgendamento);
            await _context.SaveChangesAsync();
            return Ok(novoAgendamento);
        }

        [HttpGet]
        public async Task<ActionResult> GetAgendamento()
        {
            var agendamentos = await _context.Agendamentos
                .Select(a => new
                {
                    a.Id,
                    a.DataAgendamento,
                    a.Servico,
                    a.AlunoId,
                    NomeAluno = _context.Alunos
                        .Where(al => al.Id == a.AlunoId)
                        .Select(al => al.NomeAluno)
                        .FirstOrDefault(),
                    CPFAluno = _context.Alunos
                        .Where(al => al.Id == a.AlunoId)
                        .Select(al => al.CPFAluno)
                        .FirstOrDefault(),
                    a.PersonalId,
                    NomePersonal = _context.Personais
                        .Where(p => p.Id == a.PersonalId)
                        .Select(p => p.NomePersonal)
                        .FirstOrDefault(),
                    CPFPersonal = _context.Personais
                        .Where(p => p.Id == a.PersonalId)
                        .Select(p => p.CPFPersonal)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Ok(agendamentos);
        }

        [HttpPut]
        public async Task<ActionResult> Atualizar(Agendamento agt)
        {
            var agendamentoExistente = await _context.Agendamentos.FindAsync(agt.Id);
            if (agendamentoExistente == null) return NotFound("Agendamento não encontrado.");

            var alunoExiste = await _context.Alunos.AnyAsync(a => a.Id == agt.AlunoId);
            if (!alunoExiste) return BadRequest($"Aluno com ID {agt.AlunoId} não foi encontrado.");

            var personalExiste = await _context.Personais.AnyAsync(p => p.Id == agt.PersonalId);
            if (!personalExiste) return BadRequest($"Personal com ID {agt.PersonalId} não foi encontrado.");

            agendamentoExistente.DataAgendamento = agt.DataAgendamento;
            agendamentoExistente.Servico = agt.Servico;
            agendamentoExistente.AlunoId = agt.AlunoId;
            agendamentoExistente.PersonalId = agt.PersonalId;

            await _context.SaveChangesAsync();
            return Ok(agendamentoExistente);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deletar(int id)
        {
            var agt = await _context.Agendamentos.FindAsync(id);
            if (agt != null)
            {
                _context.Agendamentos.Remove(agt);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}