using IronFitsApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace IronFitsAPI.Models
{
    public class Admin
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string NomeAdmin { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        [JsonIgnore]
        public string? Roles { get; set; } = "Admin";

    }
}
