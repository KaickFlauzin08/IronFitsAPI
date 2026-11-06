using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IronFitsApi.Models
{
    public class Personal
    {
        [Key]
        public int Id { get; set; }
        public string? NomePersonal { get; set; }
        public string CPFPersonal { get; set; }
        [JsonIgnore]
        public string? Roles { get; set; } = "Personal";
    }
}