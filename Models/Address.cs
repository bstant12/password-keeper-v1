using System;
using System.ComponentModel.DataAnnotations;

namespace PasswordKeeper.Models
{
    public class Address
    {
        [Key]

        [Required(ErrorMessage="Please enter a name for this address")]
        [Display(Name="Name:")]
        [MinLength(2, ErrorMessage="Please enter a valid name")]
        public string AddressName { get; set; }

///////////////////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage="Please enter a street address.")]
        [Display(Name="Street Address:")]
        [MinLength(2, ErrorMessage="Please enter a valid address")]
        public string StreetAddressOnw { get; set; }

///////////////////////////////////////////////////////////////////////////////////////

        [Display(Name="Appartment/Unit Number:")]
        public string StreetAddressTwo { get; set; }

///////////////////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage="Please enter a City.")]
        [Display(Name="Cit:")]
        [MinLength(2, ErrorMessage="Please enter a valid city")]
        public string City { get; set; }

///////////////////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage="Please enter a State.")]
        [Display(Name="State:")]
        [MinLength(2, ErrorMessage="Please enter a valid state")]
        public string State { get; set; }

///////////////////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage="Please enter a Zip Code.")]
        [Display(Name="Zip Code:")]
        [MinLength(7, ErrorMessage="Please enter a valid zip code")]
        [MaxLength(7, ErrorMessage="Please enter a valid zip code")]
        public string ZipCode { get; set; }

///////////////////////////////////////////////////////////////////////////////////////

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

///////////////////////////////////////////////////////////////////////////////////////



    }
}