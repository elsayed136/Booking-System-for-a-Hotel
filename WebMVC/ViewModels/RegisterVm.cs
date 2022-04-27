using System.ComponentModel.DataAnnotations;

namespace WebMVC.ViewModels
{
    public class RegisterVm
    {
            [Required, StringLength(100)]
            public string FirstName { get; set; }

            [Required, StringLength(100)]
            public string LastName { get; set; }

            [Required, StringLength(50)]
            public string Username { get; set; }

            [Required,EmailAddress, StringLength(128)]
            public string Email { get; set; }

            [Required, StringLength(256), DataType(DataType.Password)]
            public string Password { get; set; }
        
    }
}
