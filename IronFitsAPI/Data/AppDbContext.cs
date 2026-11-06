using IronFitsApi.Models;
using IronFitsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IronFitSApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Personal> Personais { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
