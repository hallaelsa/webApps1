using System;

namespace Oblig1theAteam.DBModels
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual OrderItem OrderItem { get; set; }
        public virtual User User { get; set; }
    }
}