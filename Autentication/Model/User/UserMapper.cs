using Autentication.Data;
using System;

namespace Authentication.Model.User
{
    public class UserMapper
    {
        public ApplicationUser AddUserToApplicationUser(AddUserModel addUser)
        {
            return new ApplicationUser()
            {
                UserName = addUser.Login,
                Email = addUser.Email,
                FirstName = addUser.FirstName,
                LastName = addUser.LastName,
                Status = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };
        }
    }
}
