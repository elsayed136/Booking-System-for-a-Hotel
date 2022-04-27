﻿using System.ComponentModel.DataAnnotations;

namespace WebMVC.ViewModels
{
    public class LoginVm
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
