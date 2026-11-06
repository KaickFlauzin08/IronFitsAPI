namespace IronFitsApi.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string Status { get; set; }

        public int AlunoId { get; set; }
        public Aluno? Aluno { get; set; }
        public List<Aluno>? Alunos { get; set; }

        public int PersonalId { get; set; }
        public Personal? Personal { get; set; }
        public List<Personal>? Personais { get; set; }


    }
}
