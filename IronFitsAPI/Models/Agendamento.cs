using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IronFitsApi.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string Servico { get; set; }

        public int AlunoId { get; set; }

        public int PersonalId { get; set; }

        [JsonIgnore]
        public Aluno? Aluno { get; set; }
        [JsonIgnore]
        public Personal? Personal { get; set; }

    }
}
