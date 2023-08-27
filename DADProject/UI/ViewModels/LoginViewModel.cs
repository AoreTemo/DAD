using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace UI.ViewModels;

    public class LoginViewModel
    {
        [Display(Name = "EmailAddress")]
        [Required(ErrorMessage = "Email adress field is required for filling")]    
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }

