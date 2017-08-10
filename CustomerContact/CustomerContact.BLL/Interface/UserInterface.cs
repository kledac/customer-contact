using CustomerContact.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerContact.BLL.Interface
{
    internal interface UserInterface
    {
        /// <summary>
        /// Get user by it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserSys GetUserById(int id);
        /// <summary>
        /// Return the user associated to an email and a password or NULL if user not found
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserSys Login(string email, string password);

        List<UserSys> GetAllUsers(UserRole role = null);

    }
}
