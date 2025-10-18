using System.ComponentModel.DataAnnotations;

namespace Company.Route.PL.DTOs
{
    public class ResetPasswordDto
    {

        [Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password Does not match The Password !!")]
        public string ConfirmPassword { get; set; }

    }
}
