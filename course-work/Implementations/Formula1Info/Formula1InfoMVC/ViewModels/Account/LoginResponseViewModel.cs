using System;
namespace Formula1InfoMVC.ViewModels.Account
{
    public class LoginResponseViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime? Expiration { get; set; }
    }
}

