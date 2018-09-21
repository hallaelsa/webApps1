using Microsoft.EntityFrameworkCore;
using Oblig1theAteam.Business.Users.Models;

namespace Oblig1theAteam.DBModels
{
    public class DbService : DbContext
    {
        public DbService(DbContextOptions<DbService> options)
            : base(options)
        { }
        // HUSK! Modeller er kun data, de gjør ingenting. Ikke bland modellklasser og logikklasser.
        // psudocode av ef/dbcontext
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Oblig1theAteam.Business.Users.Models.User> User { get; set; }


        // db-modeller og business-modeller ser ofte like ut i starten. Men ikke gjenbruk de bare fordi de ser like ut!
        // De endrer seg ofte over tid, og da er det kjipt å være låst til det.


    }
}
