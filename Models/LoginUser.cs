using System.ComponentModel.DataAnnotations;

namespace PasswordKeeper.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage="Email Required.")]
        [EmailAddress]
        [Display(Name="Email: ")]
        public string LoginEmail {get;set;}

        [Required(ErrorMessage="Password Required.")]
        [DataType(DataType.Password)]
        [Display(Name="Password: ")]
        public string LoginPassword {get;set;}
    }
}