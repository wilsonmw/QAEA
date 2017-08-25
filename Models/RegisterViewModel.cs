using System.ComponentModel.DataAnnotations;

namespace qaea.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(2)]
        [Display(Name="First Name")]
        public string FirstName {get; set;}
        [Required]
        [MinLength(2)]
        [Display(Name="Last Name")]
        public string LastName {get; set;}
        [Required]
        [MinLength(3)]
        public string Email {get; set;}
        [Required]
        [MinLength(8)]
        [Display(Name="Password")]
        public string Password {get; set;}
        [Required]
        [Compare("Password", ErrorMessage="Confirm Password field must match Password field.")]
        [Display(Name="Confirm Password")]
        public string PWConfirm {get; set;}

    }
}