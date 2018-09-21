using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1theAteam.Business.Users.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2013")]
        public DateTime Birthday { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
