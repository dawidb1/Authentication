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
                SecurityStamp = Guid.NewGuid().ToString()
            };
        }
    }
}
