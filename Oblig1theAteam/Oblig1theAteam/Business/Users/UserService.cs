using System.Collections.Generic;
using System.Linq;

namespace Oblig1theAteam.Business.Users
{
    public class UserService
    {
        private readonly DBModels.DbService dbService;

        public UserService(DBModels.DbService dbService)
        {
            this.dbService = dbService;
        }

        public Models.User Get(int id)
        {
            var dbUser = dbService.Users.First(u => u.Id == id);
            return ToUser(dbUser);
        }

        public List<Models.User> List()
        {
            return dbService.Users
                .Select(dbUser => ToUser(dbUser))
                .ToList();
        }

        // dette er en måte å konvertere objekter på. Kunne også vært laget som en Extension (Se OrderService)
        private Models.User ToUser(DBModels.User dbUser)
        {
            return new Models.User
            {
                Id = dbUser.Id,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName
            };
        }
    }
}
