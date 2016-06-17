using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Model;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.Validation;
using System.Threading;
using DAL;

namespace BL
{
    public class Manager
    {
        private string HashPassword(string password)
        {
            const int HASH_REPITITION = 10000;
            SHA1 sha1 = SHA1.Create();
            byte[] encryptedBytes = Encoding.UTF8.GetBytes(password);
            for (int i = 0; i < HASH_REPITITION; i++)
            {
                encryptedBytes = sha1.ComputeHash(encryptedBytes);
            }
            return Encoding.UTF8.GetString(encryptedBytes);
        }

        private void EncryptUserPassword(User user)
        {
            user.HashedPassword = HashPassword(user.Password);
            user.Password = null;
            GC.Collect();
        }

        private void RestoreRawPassword(User user, string rawPassword)
        {
            user.Password = rawPassword;
            user.HashedPassword = null;
        }

        private bool ValidatePassword(string password)
        {
            return password != null && password.Length >= 4;
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Users.Include(u => u.Roles).ToArray();
            }
        }

        public User GetUserById(int id)
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Users.Find(id);
            }
        }

        public void SaveNewUser(User user)
        {
            using (ModelContext context = new ModelContext())
            {
                context.Users.Attach(user);
                context.Entry(user).State = EntityState.Added;
                IEnumerable<DbEntityValidationResult> errors = context.GetValidationErrors();
                if (errors.Count() > 0 && !errors.All(r => r.ValidationErrors.All(e => e.PropertyName == "HashedPassword")))
                {
                    throw new DbEntityValidationException("There are errors in the user object. See EntityValidationErrors property for more details.", errors);
                }
                if (!ValidatePassword(user.Password))
                {
                    throw new ArgumentException("The supplied password is not valid", "user");
                }

                string rawPassword = user.Password;
                ICollection<Role> roles = user.Roles;
                try
                {
                    EncryptUserPassword(user);
                    user.Roles = null;
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    RestoreRawPassword(user, rawPassword);
                    throw;
                }
                finally
                {
                    user.Roles = roles;
                    rawPassword = null;
                    GC.Collect();
                }
            }
        }

        public bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;

            using (ModelContext context = new ModelContext())
            {
                User user = context.Users.SingleOrDefault(u => u.UserName.ToLower() == username.ToLower());
                if (user == null) return false;
                return HashPassword(password) == user.HashedPassword;
            }
        }

        public bool IsUsernameExists(string username)
        {
            using (ModelContext context = new ModelContext())
            {
                return context.Users.Any(u => u.UserName == username);
            }
        }
    }
}
