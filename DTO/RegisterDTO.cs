using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
       [StringLength(11)]
        public string Phone { get; set; }

        public string Address { get; set; }

    }
}
