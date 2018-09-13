using System.Collections.Generic;

namespace Oblig1theAteam.DBModels
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual IList<Order> Orders { get; set; }
    }
}