using UserRegistration.WebApi.Models;
using UserRegistration.WebApi.Util;

namespace UserRegistration.WebApi.Data
{
    public static class DbInitialize
    {
        public static void InitializeUsers()
        {
            var context = new UserRegistrationContext();

            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Login = "admin",
                    Name = "Administrator",
                    Password = "admin".Encrypt()
                });

                context.SaveChanges();
            }
        }
    }
}
