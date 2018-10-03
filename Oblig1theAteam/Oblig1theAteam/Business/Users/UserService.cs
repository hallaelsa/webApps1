using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using Oblig1theAteam.Controllers;
using Oblig1theAteam.DBModels;

namespace Oblig1theAteam.Business.Users
{
    public class UserService
    {
        private readonly DBModels.DbService dbService;
        public static CultureInfo NorwegianCultureInfo = new CultureInfo("nb-NO");

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

        private static string FirstLetterToUpper(string s)
        {
            if (!string.IsNullOrEmpty(s)) { 
                return char.ToUpper(s[0]) + s.Substring(1).ToLower();
            }
            return string.Empty;
        }

        private static string AllLettersTOLower(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return s.ToLower();
            }
            return string.Empty;
        }

        public int GetAge(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
                return 100; // if user is not logged in, user can see all movies

            var user = GetUser(email);
            var agespan = DateTime.Now - user.BirthdayDateTime;
            var age = (DateTime.MinValue + agespan).Year - 1;

            return age;
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
                BirthdayDateTime = dbUser.Birthday,
                PhoneNumber = dbUser.PhoneNumber
            };
        }

        public bool CreateUser(Business.Users.Models.User newUser)
        {

            try
            {
                bool userExists = UserExists(newUser.Email);

                if (!userExists)
                {

                    byte[] salt = CreateSalt();
                    var user = new DBModels.User()
                    {
                        Email = AllLettersTOLower(newUser.Email),
                        FirstName = FirstLetterToUpper(newUser.FirstName),
                        LastName = FirstLetterToUpper(newUser.LastName),
                        Birthday = DateTime.ParseExact(newUser.Birthday, "dd.MM.yyyy", NorwegianCultureInfo),
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

        public bool UserExists(string email)
        {
            var emailAllLower = AllLettersTOLower(email);
            return dbService.Users
                    .Any(u => u.Email == emailAllLower);
        }

        public static implicit operator UserService(UserController v)
        {
            throw new NotImplementedException();
        }
    }
}
