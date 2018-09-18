using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Oblig1theAteam.DBModels
{
    public class User
    {
        [Key]
        public int Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public virtual IList<Order> Orders { get; set; }
    }
}