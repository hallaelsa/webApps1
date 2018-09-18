using Microsoft.EntityFrameworkCore;

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


        // db-modeller og business-modeller ser ofte like ut i starten. Men ikke gjenbruk de bare fordi de ser like ut!
        // De endrer seg ofte over tid, og da er det kjipt å være låst til det.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.Movie, oi.Order });

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItem)
                .HasForeignKey(oi => oi);

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);
        }
    }
}
