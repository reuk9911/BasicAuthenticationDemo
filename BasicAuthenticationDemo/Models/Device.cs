﻿using System.ComponentModel.DataAnnotations;

namespace BasicAuthenticationDemo.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
