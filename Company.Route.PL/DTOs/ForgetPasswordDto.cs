using System.ComponentModel.DataAnnotations;

namespace Company.Route.PL.DTOs
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
