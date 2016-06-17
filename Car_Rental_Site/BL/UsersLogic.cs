using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model;


namespace BL
{
    public class UsersLogic : BaseLogic
    {
        public List<User> GetAllUsers()
        {
            return DB.Users.ToList();
        }

        public User GetOneUserDetails(string username)
        {
            return DB.Users.Where(m => m.UserName == username).FirstOrDefault();
        }

        public void EditUser(User toEdit)
        {
            DB.Users.Add(toEdit);
            DB.Entry(toEdit).State = EntityState.Modified;
            DB.SaveChanges();
        }

        public void DeleteUser(User toDelete)
        {
            DB.Entry(toDelete).State = EntityState.Deleted;
            DB.SaveChanges();
        }

        //public int ResetPasswordManager(string username, string newPassword)
        //{
        //    return DB.resetPasswordForManager(username, newPassword);
        //}
        public bool CheckUsername(string username)
        {
            var IsExists = DB.Users.Where(u => u.UserName == username).FirstOrDefault();
            if (IsExists!=null)
            {
                return false;
            }
            return true;
        }



    }
}
