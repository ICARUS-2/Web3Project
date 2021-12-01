using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Models
{
    public class CreateUserModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "The field First Name must be a string with a minimum length of 1 and a maximum length of 50.")]
        [MaxLength(50, ErrorMessage = "The field First Name must be a string with a minimum length of 1 and a maximum length of 50.")]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "First name cannot contain numbers.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "The field Last Name must be a string with a minimum length of 1 and a maximum length of 50.")]
        [MaxLength(50, ErrorMessage = "The field Last Name must be a string with a minimum length of 1 and a maximum length of 50.")]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "Last name cannot contain numbers.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@".*@.*\.\w{2,}", ErrorMessage = "The email field is not a valid email address.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"\b(?:(?!Select Role)\w)+\b", ErrorMessage = "Choose a Role")]
        public string Role { get; set; }
    }
}
