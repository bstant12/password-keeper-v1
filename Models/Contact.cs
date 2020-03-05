using System;
using System.ComponentModel.DataAnnotations;

namespace PasswordKeeper.Models
{
    public class Contact
    {
        [Key]

        public int ContactId { get; set; }

        //////////////////////////////////////////

        [Required(ErrorMessage="A contact needs a name.")]
        [Display(Name="First Name:")]
        [MinLength(2, ErrorMessage="A first name needs at least 2 characters.")]
        public string CFirstName { get; set; }

        //////////////////////////////////////////

        [Display(Name="Last Name:")]
        [MinLength(2, ErrorMessage="A name needs at least 2 characters.")]
        public string CLastName { get; set; }

        //////////////////////////////////////////

        [Display(Name="Phone Numer:")]
        [MinLength(10, ErrorMessage="A phone number has 10 numbers.")]
        [MaxLength(10, ErrorMessage="A phone number has 10 numbers.")]
        public string CPhoneNumber { get; set; }

        //////////////////////////////////////////

        [Display(Name="Email:")]
        [EmailAddress]
        public string CEmail { get; set; }

        //////////////////////////////////////////

        [Display(Name="Relationship:")]
        public string CRelationship { get; set; }

        //////////////////////////////////////////

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        //////////////////////////////////////////

        // Navigation Props
        // One To Many - USER
        public int UserId { get; set; }
        public User ContactMaker { get; set; }


        //////////////////////////////////////////

        public class PastDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {

                DateTime check;

                if(value is DateTime)
                {
                    check = (DateTime)value;

                    if(value == null)
                    {
                        return ValidationResult.Success;
                    }
                    else if(check > DateTime.Now)
                    {
                        return new ValidationResult("Their Birthday Needs to be in the past");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
                else
                {
                    return new ValidationResult("Enter a real date yo.");
                }
            }
        }
    }
}