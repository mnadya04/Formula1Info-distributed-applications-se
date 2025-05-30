using System;
namespace Formula1InfoMVC.ViewModels.Account
{
    public class LoginResult
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuccess => !string.IsNullOrEmpty(UserId);
    }
}

