using System.ComponentModel.DataAnnotations;

namespace IronFitsApi.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string Status { get; set; }

        public int AlunoId { get; set; }

        public int PersonalId { get; set; }



    }
}
