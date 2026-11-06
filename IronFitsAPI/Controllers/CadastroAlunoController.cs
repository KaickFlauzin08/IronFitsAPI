using IronFitsApi.Models;
using IronFitSApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IronFitsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroAlunoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CadastroAlunoController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult> CriarAluno(Aluno aluno)
        {
            var novoaluno = new Aluno
            {
                CPFAluno = aluno.CPFAluno,
                NomeAluno = aluno.NomeAluno,
                Roles = "Aluno"
            };
            _context.Alunos.Add(novoaluno);
            await _context.SaveChangesAsync();
            return Ok(novoaluno);
        }

        [HttpGet]
        public async Task<ActionResult<List<Aluno>>> GetAluno()
        {
            var aluno = await _context.Alunos
                .Select(a => new Aluno
                {
                    Id = a.Id,
                    NomeAluno = a.NomeAluno,
                    CPFAluno= a.CPFAluno
                }).ToListAsync();
            return Ok(aluno);
        }
    }
}
