using System;
using System.ComponentModel.DataAnnotations;

namespace PasswordKeeper.Models
{
    public class Note
    {
        [Key]
        public int NoteId { get; set; } 

        [Required(ErrorMessage="Please name your note.")]
        [Display(Name="Note Name:")]
        [MinLength(1, ErrorMessage="Please name your note.")]
        public string Name { get; set; }

        ////////////////////////////////////////////

        [Required(ErrorMessage="Please enter your notes.")]
        [Display(Name="Note Content:")]
        [MinLength(1, ErrorMessage="Please enter your notes.")]
        public string NoteContent { get; set; }

        ///////////////////////////////////////////

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        //////////////////////////////////////////

        // Navigation Props
        // One To Many - USER
        public int UserId { get; set; }
        public User NoteTaker { get; set; }  
    }
}