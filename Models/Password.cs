using System;
using System.ComponentModel.DataAnnotations;

namespace PasswordKeeper.Models
{
    public class Password
    {
        [Key]
        public int PasswordId { get; set; }

        //////////////////////////////////////////////////

        [Required(ErrorMessage="Please enter a website")]
        [MinLength(2, ErrorMessage="Please enter a real website")]
        [Display(Name="Website:")]
        public string Website { get; set; }

        //////////////////////////////////////////////////

        [Required(ErrorMessage="Please enter associated Email")]
        [Display(Name="Email:")]
        [EmailAddress]
        public string Email { get; set; }

        //////////////////////////////////////////////////

        [Display(Name="Username:")]
        public string UserName { get; set; }

        //////////////////////////////////////////////////
        
        [Required(ErrorMessage="Please enter Your Password for this site")]
        [MinLength(8, ErrorMessage="Please a stronger password")]
        [Display(Name="Password:")]
        public string ThePassword { get; set; }

        //////////////////////////////////////////////////

        [Display(Name="Password Notes:")]
        public string PasswordNotes { get; set; }

        //////////////////////////////////////////////////

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation Props
        // One To Many - USER
        public int UserId { get; set; }
        public User PasswordMaker { get; set; }  
    }
}