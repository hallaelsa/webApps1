using System;
using System.Collections.Generic;
using System.Linq;
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
            var userExists = dbService.Users
                .Any(u => u.Email == email && u.Password == password);

            return userExists;
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
                Birthday = dbUser.Birthday,
                Password = dbUser.Password,
                PhoneNumber = dbUser.PhoneNumber
            };
        }

        public static implicit operator UserService(UserController v)
        {
            throw new NotImplementedException();
        }
    }
}
