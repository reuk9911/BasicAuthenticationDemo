using System.ComponentModel.DataAnnotations;

namespace BasicAuthenticationDemo.Models
{
    public class Device
    {
        /// <summary>
        /// Id устройства 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Тип устройства 
        /// </summary>
        [Required]
        public string Type { get; set; }
    }
}
