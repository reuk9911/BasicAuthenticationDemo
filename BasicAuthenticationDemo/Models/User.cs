using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BasicAuthenticationDemo.Models
{
    [Index(nameof(Email), Name = "Index_Email_Unique", IsUnique = true)]
    public class User
    {
        /// <summary>
        /// Id 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Имя 
        /// </summary>
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}