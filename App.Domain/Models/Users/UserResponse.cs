using App.Domain.Entities.Users;
using System;

namespace App.Domain.Models.Users
{
    public class UserResponse
    {
        public UserResponse()
        {
        }

        public UserResponse(User model)
        {
            Id = model.Id;
            UserName = model.UserName;
            Email = model.Email;
            PhoneNumber = model.PhoneNumber;
            FirstName = model.FirstName;
            LastName = model.LastName;
            CreateDate = model.CreateDate;
            Age = model.Age;
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; set; }
        public int Age { get; set; }
    }
}
