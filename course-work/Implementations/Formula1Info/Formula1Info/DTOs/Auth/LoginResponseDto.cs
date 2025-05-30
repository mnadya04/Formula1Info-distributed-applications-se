using System;
namespace Formula1Info.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}

