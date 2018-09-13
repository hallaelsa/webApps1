using System;

namespace Oblig1theAteam.DBModels
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
    }
}