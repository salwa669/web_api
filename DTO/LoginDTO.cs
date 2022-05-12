using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTO
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
