using System;
using System.ComponentModel.DataAnnotations;

namespace PasswordKeeper.Models
{
    public class PersonalInformation
    {
        [Key]
        public int PersonalInformationId { get; set; }

/////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage="Please Title The Info")]
        [Display(Name="Information Type:")]
        [MinLength(1, ErrorMessage="Please enter something")]
        public string InfoName { get; set; }

/////////////////////////////////////////////////////////////////////

        [Required(ErrorMessage="Please Enter Your Info")]
        [Display(Name="Information Content:")]
        [MinLength(1, ErrorMessage="Please enter something")]
        public string InfoContent { get; set; }

/////////////////////////////////////////////////////////////////////

        [Display(Name="Info Notes:")]
        public string InfoNote { get; set; }

/////////////////////////////////////////////////////////////////////

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

//////////////////////////////////////////////////////////////////////////

        // Navigation Props
        // One To Many - USER
        public int UserId { get; set; }
        public User InfoOwner { get; set; }  
    }
}