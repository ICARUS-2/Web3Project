using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Models
{
    public class ContactModel
    {
        [Key]
        public int Id { get; set; }

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
        public string Topic { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Questions or Comments")]
        public string Message { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
