namespace IronFitsApi.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string Status { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public List<Cliente>? Clientes { get; set; }

        public int PersonalId { get; set; }
        public Personal? Personal { get; set; }
        public List<Personal>? Personais { get; set; }


    }
}
