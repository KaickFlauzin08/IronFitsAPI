using IronFitsApi.Models;
using IronFitSApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace IronFitsAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
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
                PersonalId = aluno.PersonalId,
                Roles = aluno.Roles
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
                    CPFAluno = CpfCms.Criptografar(a.CPFAluno),
                    PersonalId = a.PersonalId,
                    Roles = a.Roles
                }).ToListAsync();

            return Ok(aluno);
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarAluno(Aluno aluno)
        {
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        // CRIPTOGRAFIA DE CPF
        public static class CpfCms
        {
            private static readonly X509Certificate2 Cert = CriarCertificado();

            private static X509Certificate2 CriarCertificado()
            {
                using (var rsa = RSA.Create(2048))
                {
                    var request = new CertificateRequest(
                        new X500DistinguishedName("CN=Key"),
                        rsa,
                        HashAlgorithmName.SHA256,
                        RSASignaturePadding.Pkcs1
                    );
                    return request.CreateSelfSigned(DateTimeOffset.Now.AddDays(-1), DateTimeOffset.Now.AddYears(5));
                }
            }

            public static string Criptografar(string cpf)
            {
                if (string.IsNullOrEmpty(cpf)) return cpf;
                var cms = new EnvelopedCms(new ContentInfo(Encoding.UTF8.GetBytes(cpf)));
                cms.Encrypt(new CmsRecipient(Cert));
                return Convert.ToBase64String(cms.Encode());
            }

            public static string Decodificar(string cpfCriptografado)
            {
                if (string.IsNullOrEmpty(cpfCriptografado)) return cpfCriptografado;
                var cms = new EnvelopedCms();
                cms.Decode(Convert.FromBase64String(cpfCriptografado));
                cms.Decrypt(new X509Certificate2Collection(Cert));
                return Encoding.UTF8.GetString(cms.ContentInfo.Content);
            }
        }
    }
}