using System.Text.Json.Serialization;

namespace IronFitsApi.Models
{
    public class Aluno
    {
        public int? Id { get; set; }
        public string NomeAluno { get; set; }
        public string CPFAluno { get; set; }
        public string? Roles { get;set; }
    }
}
