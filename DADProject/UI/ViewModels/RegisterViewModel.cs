using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels;

public class RegisterViewModel
{
    [Display(Name = "Username")]
    [Required(ErrorMessage = "UserName is required to fill")]
    public string Username { get; set; }

    [Display(Name = "First name")]
    [Required(ErrorMessage ="First name is required to fill")]
    public string FirstName { get; set; }

    [Display(Name = "Last name")]
    [Required(ErrorMessage = "Last name is required to fill")]
    public string LastName { get; set; }

    [Display(Name = "Email address")]
    [Required(ErrorMessage = "Email is required to fill")]
    [DataType(DataType.EmailAddress)]
    public string EmailAddress { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage ="Password is required to fill")]
    public string Password { get; set; }

    [Display(Name = "Confirm password")]
    [Required(ErrorMessage ="Confirm password field is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage ="Password don't match")]
    public string ConfirmPassword { get; set; }
}

