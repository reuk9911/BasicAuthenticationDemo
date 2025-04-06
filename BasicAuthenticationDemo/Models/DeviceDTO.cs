using System.ComponentModel.DataAnnotations;

namespace BasicAuthenticationDemo.Models
{
    public class DeviceDTO
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
