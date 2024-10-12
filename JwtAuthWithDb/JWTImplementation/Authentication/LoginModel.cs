using System.ComponentModel.DataAnnotations;

namespace JWTImplementation.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User name is required!")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }
    }
}
