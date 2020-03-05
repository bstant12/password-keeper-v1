using System;
using System.ComponentModel.DataAnnotations;

namespace PasswordKeeper.Models
{
    public class CreditCard
    {
        [Key]
        public int CreditCardId { get; set; }

        ///////////////////////////////////////////////

        [Required(ErrorMessage="Please name this card.")]
        [MinLength(1, ErrorMessage="Please name this card")]
        [Display(Name="Card Name")]
        public string CardName { get; set; }

/////////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage="What is the card number.")]
        [Display(Name="Card Number")]
        public string CardNumber { get; set; }

//////////////////////////////////////////////////////////////////////////////

        public string CardType { get; set; }

//////////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage="Please enter an expiration date.")]
        [Display(Name="Expiration Date")]
        [PastDate]
        [DataType(DataType.Date)]
        public DateTime Expiration { get; set; }

//////////////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage="Please enter a valid Security Code.")]
        [Display(Name="Security Code")]
        
        public string SecurityCode { get; set; }

//////////////////////////////////////////////////////////////////////////////

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

//////////////////////////////////////////////////////////////////////////////

        // Navigation Props
        // One To Many - USER
        public int UserId { get; set; }
        public User CardHolder { get; set; }  

//////////////////////////////////////////////////////////////////////////////

    }

    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime check;
            if(value is DateTime)
            {
                check = (DateTime)value;

                if(check.Year < DateTime.Now.Year && check.Month < DateTime.Now.Month )
                {
                    return new ValidationResult("Your Credit Card is expired");
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