using System.ComponentModel.DataAnnotations;

namespace BasicAuthenticationDemo.DTOs
{
    public class DeviceDTO
    {
        /// <summary>
        /// Id устройства 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Тип устройства 
        /// </summary>
        [Required]
        public string Type { get; set; }
    }
}
