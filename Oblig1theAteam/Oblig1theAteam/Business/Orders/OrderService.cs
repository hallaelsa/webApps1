using Oblig1theAteam.DBModels;
using Oblig1theAteam.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Logikklasser gjør ting. De skal ikke holde på data, kun bruke dem.
namespace Oblig1theAteam.Business.Orders
{
    public class OrderService
    {
        private readonly DbService dbService;

        public OrderService(DbService dbService)
        {
            this.dbService = dbService;
        }

        public bool saveOrder()
        {
            return true;
        }

        //Her bruker vi Extentions for å gjøre om DbModell til business modell.
        //public Models.Order Get(int id)
        //{
        //    var dbOrder = dbService.Orders.First(o => o.Id == id);
        //    return dbOrder.ToOrder();
        //}

        public List<Models.Order> GetOrders(string email)
        {
            var orders = dbService.Orders
                .Where(o => o.User.Email == email)
                .Select(o => o.ToOrder())
                .ToList();

            foreach(var order in orders)
            {
                order.Movies = GetMovies(order.Id);
            }
            return orders;
        }

        public List<Movie> GetMovies(int orderId)
        {
            return dbService.OrderItem
            .Where(oi => oi.Order.Id == orderId)
            .Select(oi => ToMovie(oi.Movie))
            .ToList();
        }

        private Movie ToMovie(Movie dbMovie)
        {
            return new Movie
            {
                Id = dbMovie.Id,
                Title = dbMovie.Title,
                Year = dbMovie.Year,
                AgeRestriction = dbMovie.AgeRestriction,
                Time = dbMovie.Time,
                Description = dbMovie.Description,
                TrailerLink = dbMovie.TrailerLink,
            };
        }

        public void CreateOrder()
        {

        }

        /*
         * public bool settInn(Kunde innKunde)
        {
            var nyKunde = new Kunder()
            {
                Fornavn = innKunde.fornavn,
                Etternavn = innKunde.etternavn,
                Adresse = innKunde.adresse,
                Postnr = innKunde.postnr
            };

            var db = new KundeContext();
            try
            {  
                var eksistererPostnr = db.Poststeder.Find(innKunde.postnr);
               
                if (eksistererPostnr == null)
                {
                    var  nyttPoststed = new Poststeder()
                    {
                        Postnr = innKunde.postnr,
                        Poststed = innKunde.poststed
                    };
                    nyKunde.Poststeder = nyttPoststed;
                }
                db.Kunder.Add(nyKunde);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                return false;
            }
        }
        */

        //public List<Models.Order> ListByDate(DateTime date)
        //{
        //    return dbService.Orders
        //        .Where(o => o.Date.Date == date)
        //        .OrderBy(o => o.Date)
        //        .Select(dbOrder => dbOrder.ToOrder())
        //        .ToList();
        //}
    }
}
