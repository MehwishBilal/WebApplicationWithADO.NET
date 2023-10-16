using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplicationWithADO.NET.Models
{
    public class user
    {
       // [JsonIgnore]
        [Key]
        public int Userid { get; set; }

        [NotMapped]
        public string? Encryptedid { get; set; } //Encrypted id value
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public bool isActive { get; set; }
    }
}
