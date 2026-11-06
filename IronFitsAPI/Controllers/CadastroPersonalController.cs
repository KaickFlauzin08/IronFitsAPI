using IronFitsApi.Models;
using IronFitSApi.Data;
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
    [ApiController]
    public class CadastroPersonalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CadastroPersonalController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CriarPersonal(Personal personal)
        {
            _context.Personais.Add(personal);
            await _context.SaveChangesAsync();
            return Ok(personal);
        }

        [HttpGet]
        public async Task<ActionResult<List<Personal>>> GetPersonal()
        {
            var personal = await _context.Personais
                .Select(p => new Personal
                {
                    Id = p.Id,
                    NomePersonal = p.NomePersonal,
                    CPFPersonal = CpfCms.Criptografar(p.CPFPersonal),
                    Roles = p.Roles
                }).ToListAsync();

            return Ok(personal);
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarPersonal(Personal personal)
        {
            _context.Personais.Update(personal);
            await _context.SaveChangesAsync();
            return Ok(personal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarPersonal(int id)
        {
            var personal = await _context.Personais.FindAsync(id);
            _context.Personais.Remove(personal!);
            await _context.SaveChangesAsync();
            return Ok();
        }



        //CRIPTOGRAFIA DE CPF
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