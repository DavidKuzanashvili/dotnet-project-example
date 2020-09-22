using Microsoft.AspNetCore.Identity;
using System;

namespace App.Domain.Models.Users
{
    public class User : IdentityUser
    {
        public User()
        {
            FullName = $"{FirstName} {LastName}";
            CreateDate = DateTime.Now;
        }

        public int Age { get; set; }
        public DateTime CreateDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }
}
