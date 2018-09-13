using Microsoft.EntityFrameworkCore;

namespace Oblig1theAteam.DBModels
{
    public class DbService
    {
        // HUSK! Modeller er kun data, de gjør ingenting. Ikke bland modellklasser og logikklasser.
        // psudocode av ef/dbcontext
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        // db-modeller og business-modeller ser ofte like ut i starten. Men ikke gjenbruk de bare fordi de ser like ut!
        // De endrer seg ofte over tid, og da er det kjipt å være låst til det.
    }
}
