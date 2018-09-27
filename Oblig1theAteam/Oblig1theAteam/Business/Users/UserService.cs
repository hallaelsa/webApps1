using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Oblig1theAteam.Controllers;
using Oblig1theAteam.DBModels;

namespace Oblig1theAteam.Business.Users
{
    public class UserService
    {
        private readonly DBModels.DbService dbService;

        public UserService(DBModels.DbService dbService)
        {
            this.dbService = dbService;
        }

        public Models.User GetUser(string email)
        {
            var dbUser = GetDbUser(email);
            return ToUser(dbUser);
        }

        public User GetDbUser(string email)
        {
            return dbService.Users.First(u => u.Email == email);
        }

        public bool Login(string email, string password)
        {
            // her må vi hashe passord!!!!!!!!!!!!!!!!
            var user = dbService.Users
                .FirstOrDefault(u => u.Email == email);
            
            if(user != null)
            {
                return VerifyPassword(user, password);
            } else
            {
                return false;
            }
            
        }

        private bool VerifyPassword(DBModels.User user, string enteredPassword)
        {
            byte[] userPassword =  HashPassword(enteredPassword, user.Salt);
            bool match = user.Password.SequenceEqual(userPassword);
            return match;
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            const int keyLength = 24;
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 2000);
            return pbkdf2.GetBytes(keyLength);
        }

        private byte[] CreateSalt()
        {
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csprng.GetBytes(salt);
            return salt;
        }

        public List<Models.User> ListUsers()
        {
            return dbService.Users
                .Select(dbUser => ToUser(dbUser))
                .ToList();
        }

  
        private Models.User ToUser(DBModels.User dbUser)
        {
            return new Models.User
            {
                Email = dbUser.Email,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                Birthday = dbUser.Birthday.ToShortDateString(),
                PhoneNumber = dbUser.PhoneNumber
            };
        }

        public bool CreateUser(Business.Users.Models.User newUser)
        {

            try
            {
                bool userExists = dbService.Users
                    .Any(u => u.Email == newUser.Email);

                if (!userExists)
                {
                    byte[] salt = CreateSalt();
                    var user = new DBModels.User()
                    {
                        Email = newUser.Email,
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        Birthday = DateTime.Parse(newUser.Birthday),
                        Password = HashPassword(newUser.Password, salt),
                        PhoneNumber = newUser.PhoneNumber,
                        Salt = salt
                    };
                    dbService.Add(user);
                    dbService.SaveChanges();
                    return true;
                }

                return false;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static implicit operator UserService(UserController v)
        {
            throw new NotImplementedException();
        }
    }
}
