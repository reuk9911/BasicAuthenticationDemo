using System.ComponentModel.DataAnnotations;

namespace BasicAuthenticationDemo.DTOs
{
    public class DeviceDTO
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
