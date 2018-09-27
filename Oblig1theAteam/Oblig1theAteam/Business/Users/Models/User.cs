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
        [RegularExpression(@"^[A-Z]+(([\'\,\.\-][a-z])?[a-z]*)*$", ErrorMessage = "First letter must be a large capital. Example: Charles")]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Missing last name.")]
        [Display(Name = "Last name")]
        [RegularExpression(@"^[A-Z]+(([\'\,\.\-][a-z])?[a-z]*)*$", ErrorMessage = "First letter must be a large capital. Example: Darwin")]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Missing Birthday.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy}")]
        [Range(type: typeof(DateTime), minimum: "1/1/1900", maximum: "1/1/2013")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Missing password.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Must contain at least 8 charachters combining letters and numbers.")]
        //[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).$", ErrorMessage = "Must contain letters and numbers.")]
        [StringLength(15, MinimumLength = 8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Missing phone number.")]
        [Display(Name = "Phone number")]
        [RegularExpression(@"[1-9]{1}[0-9]{7}", ErrorMessage = "Phone number must contain 8 numbers.")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
