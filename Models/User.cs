using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordKeeper.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required(ErrorMessage="You need a First Name.")]
        [MinLength(3, ErrorMessage="First Name must be at least 3 characters.")]
        [Display(Name="First Name:")]
        public string FirstName {get;set;}

        [Required(ErrorMessage="You need a Last Name.")]
        [MinLength(3, ErrorMessage="Last Name must be at least 3 characters.")]
        [Display(Name="Last Name:")]
        public string LastName {get;set;}

        [Required(ErrorMessage="You need a Email.")]
        [EmailAddress]
        [Display(Name="Email:")]
        public string Email {get;set;}

        [Required(ErrorMessage="You need a Password.")]
        [MinLength(8, ErrorMessage="Password must be at least 8 characters.")]
        [DataType(DataType.Password)]
        [Display(Name="Password:")]
        public string Password {get;set;}

        [Required(ErrorMessage="You need a Confirm Password.")]
        [DataType(DataType.Password)]
        [Display(Name="Confirm Password:")]
        [Compare("Password", ErrorMessage="Those do not match.")]
        [NotMapped]
        public string ConfirmPassword {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        
        //////////////////////////////////////////

        // Navigation Props
        ///
        // One To Many - NOTE
        public List<Note> MyNotes { get; set; }
        ///
        // One to Many - Contacts
        public List<Contact> MyContacts { get; set; }
        ///
        // // One to Many - PASSWORD
        public List<Password> MyPasswords { get; set; }
        ///
        // // One to Many - CREDIT CARD
        public List<CreditCard> MyCreditCards { get; set; }
        
         // // One to Many - CREDIT CARD
        public List<PersonalInformation> MyPersonalInformations { get; set; }
    }
}