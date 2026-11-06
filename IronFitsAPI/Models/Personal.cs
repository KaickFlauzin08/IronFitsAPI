namespace IronFitsApi.Models
{
    public class Personal
    {
        public int Id { get; set; }
        public string? NomePersonal { get; set; }
        public string CPFPersonal { get; set; } = string.Empty;
        public string? Roles { get; set; } 
    }
}