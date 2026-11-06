using IronFitsAPI.Models;
using IronFitSApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IronFitsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Regitar-Admin")]
        public async Task<ActionResult> CriarAdmin(Admin Adm)
        {
            var novoadmin = new Admin
            {
                Email = Adm.Email,
                Senha = Adm.Senha,
                Roles = Adm.Roles
            };
            _context.Admins.Add(novoadmin);
            await _context.SaveChangesAsync();
            return Ok(novoadmin);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Admin admin)
        {
            var user = await _context.Admins.FirstOrDefaultAsync(u => u.Email == admin.Email);

            if (user != null && user.Senha == admin.Senha)
            {
                var token = CriarToken(user);
                return Ok(new { token });
            }

            return Unauthorized(new { Mensagem = "Credenciais inválidas" });
        }

        private string CriarToken(Admin admin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim(ClaimTypes.Role, admin.Roles)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("minha-chave-super-segura-com-32-caracteres"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddHours(2)
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
