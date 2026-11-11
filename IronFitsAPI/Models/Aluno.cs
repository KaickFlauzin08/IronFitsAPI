using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IronFitsApi.Models
{
    public class Aluno
    {
        [Key]
        public int? Id { get; set; }
        public string NomeAluno { get; set; }
        public string CPFAluno { get; set; }
        public int PersonalId { get; set; }
        [JsonIgnore]
        public string? Roles { get;set; } = "Aluno";
    }
}
