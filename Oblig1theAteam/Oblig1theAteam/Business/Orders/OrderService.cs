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
        private readonly DBModels.DbService dbService;

        public OrderService(DBModels.DbService dbService)
        {
            this.dbService = dbService;
        }

        //Her bruker vi Extentions for å gjøre om DbModell til business modell.
        //public Models.Order Get(int id)
        //{
        //    var dbOrder = dbService.Orders.First(o => o.Id == id);
        //    return dbOrder.ToOrder();
        //}

        public List<Models.Order> ListByUser(string email)
        {
            return dbService.Orders
                .Where(o => o.User.Email == email)
                .Select(dbOrder => dbOrder.ToOrder())
                .ToList();
        }

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
