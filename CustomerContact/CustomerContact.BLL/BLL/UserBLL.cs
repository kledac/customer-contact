using CustomerContact.BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerContact.DAL;
using System.Security.Cryptography;

namespace CustomerContact.BLL
{
    public class UserBLL : BaseBLL, UserInterface
    {
        /// <summary>
        /// Return all Users, If a role is provided return only users of that role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public List<UserSys> GetAllUsers(UserRole role = null)
        {
            return role==null?context.UserSys.ToList():context.UserSys.Where(u=>u.UserRoleId==role.Id).ToList();
        }

        

        public UserSys GetUserById(int id)
        {
            return context.UserSys.FirstOrDefault(u => u.Id == id);
        }

        public UserSys Login(string email, string password)
        {
            String hashPass = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(password)).Select(s => s.ToString("x2")));
            return context.UserSys.FirstOrDefault(u => u.Email.Equals(email) && u.Password.Equals(hashPass));
        }
        /// <summary>
        /// Set all passwords to MD5
        /// </summary>
        public void SetPasswordToMD5()
        {
            foreach (var User in context.UserSys)
            {
                User.Password = string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(User.Password)).Select(s => s.ToString("x2")));
            }
            this.Save();
        }

    }
}
