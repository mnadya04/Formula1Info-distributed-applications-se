using System;
using Microsoft.AspNetCore.Identity;

namespace Formula1Info.Models
{
	public class ApplicationUser : IdentityUser<string>
	{
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

