using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public DateTime OrderDate{ get; set;}

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual List<Product> Products { get; set; }

    }
}
