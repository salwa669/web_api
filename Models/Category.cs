using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual List<Product> Products { get; set; }
    }
}
