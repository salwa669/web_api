using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ecommerce.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int CartQuantity { get; set; }

        [JsonIgnore]
        public virtual List<Product> Products { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
       

    }
}
