using App.Domain.Entities.Users;
using App.Infrastructure.Authorization.Models.Registration;

namespace App.Infrastructure.Authorization.Utils.Transformers
{
    public static class RegistrationTransformer
    {
        public static User Transform(Register model)
        {
            return new User()
            {
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
            };
        }
    }
}
