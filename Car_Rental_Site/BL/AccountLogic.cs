using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using System.Data.Entity;

namespace BL
{
    public class AccountLogic : BaseLogic
    {
        /// <summary>
        /// Check if the user succeed to log in.
        /// </summary>
        /// <param name="username">The user name</param>
        /// <param name="password">The password after encoding</param>
        /// <returns>true if the user succeed to login, false if not</returns>
        public bool IsUserExist(string username, string password)
        {
            return DB.Users.Count(m => m.UserName == username && m.Password == password) == 1;
        }

        /// <summary>
        /// Check if the username is taken.
        /// </summary>
        /// <param name="username">The username</param>
        /// <returns>true if is taken, false is not</returns>
        public bool IsUsernameTaken(string username)
        {
            return DB.Users.Count(m => m.UserName == username) == 1;
        }

        /// <summary>
        /// Get the user code - unique serial number.
        /// Use for other actions.
        /// </summary>
        /// <param name="username">The username</param>
        /// <returns></returns>
        public int GetUserID(string username)
        {
            return DB.Users.Where(m => m.UserName == username).Select(m => m.UserID).FirstOrDefault();
        }

        /// <summary>
        /// Register new user to the DB.
        /// </summary>
        /// <param name="toRegister">The user details</param>
        public void Register(User toRegister)
        {
            DB.Users.Add(toRegister);
            DB.SaveChanges();
        }

        /// <summary>
        /// Change user password - for users.
        /// Change the password only if the username and the old password match to the DB.
        /// </summary>
        /// <param name="username">The user name</param>
        /// <param name="oldPassword">The old password after encoding</param>
        /// <param name="newPassword">The new password after encoding</param>
        /// <returns>1 if succeed, 0 if not</returns>
       
        //public int ResetPassword(string username, string oldPassword, string newPassword)
        //{
        //    return DB.resetPasswordForUsers(username, oldPassword, newPassword);
        //}

        public string userRole(string userName)
        {
            return "";
        }
    }
}
