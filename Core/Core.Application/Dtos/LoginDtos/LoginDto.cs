﻿using System.ComponentModel.DataAnnotations;


namespace Core.Application.Dtos.LoginDtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
