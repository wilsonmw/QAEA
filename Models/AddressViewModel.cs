using qaea.Models;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;

namespace qaea.Models
{
    public class AddressViewModel
    {
        [Required]
        public string Address1 {get; set;}
        public string Address2 {get; set;}
        [Required]
        [MinLength(2)]
        public string City {get; set;}
        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        public string State {get; set;}
        [Required]
        [MinLength(5)]
        public int ZipCode {get; set;}

    }
}