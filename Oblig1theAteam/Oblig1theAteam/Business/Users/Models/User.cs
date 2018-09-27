using Microsoft.AspNetCore.Mvc;
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
        [Required(ErrorMessage = "Missing email.")]
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
         @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", ErrorMessage = "Email is invalid. Example: example@gmail.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Missing first name.")]
        [Display(Name = "First name")]
        [RegularExpression(@"^[A-ZÆØÅa-zæøå]+(([\'\,\.\-][a-z])?[a-z]*)*$", ErrorMessage = "First name must contain only letters. Example: Charles")]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Missing last name.")]
        [Display(Name = "Last name")]
        [RegularExpression(@"^[A-ZÆØÅa-zæøå]+(([\'\,\.\-][a-z])?[a-z]*)*$", ErrorMessage = "Last name must contain only letters. Example: Darwin")]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Missing birthday.")]
        [RegularExpression(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]|(?:Jan|Mar|May|Jul|Aug|Oct|Dec)))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2]|(?:Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)(?:0?2|(?:Feb))\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9]|(?:Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep))|(?:1[0-2]|(?:Oct|Nov|Dec)))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$", ErrorMessage = "Must contain format: dd.mm.yyyy and valid date")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Format must be dd.mm.yyyy (Example: 01.01.2010 or 01-01-2010)")]
        public string Birthday { get; set; }

        [Required(ErrorMessage = "Missing password.")]
        [RegularExpression(@"(?=^.{8,15}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$", ErrorMessage = "Between 8 and 15, contains atleast one digit, one upper case and one lower case and no whitespace.")]
        [StringLength(15, MinimumLength = 8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Missing phone number.")]
        [Display(Name = "Phone number")]
        [RegularExpression(@"[1-9]{1}[0-9]{7}", ErrorMessage = "Phone number must contain 8 numbers.")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
