using Oblig1theAteam.Business.Orders.Models;

namespace Oblig1theAteam.Extensions
{
    public static class OrderExtension
    {
        public static Order ToOrder(this DBModels.Order dbOrder)
        {
            return new Order
            {
                Id = dbOrder.Id,
                Date = dbOrder.OrderDate
            };
        }
    }
}
