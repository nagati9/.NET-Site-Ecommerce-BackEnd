﻿namespace Gestionnaire2.DTO
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nom { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
