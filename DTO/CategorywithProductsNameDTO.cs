using System.Collections.Generic;

namespace Ecommerce.DTO
{
    public class CategorywithProductsNameDTO
    {
        public int CatgegoryId { get; set; }
        public string CategoryName { get; set; }
        public List<string> ProductsName { get; set; }=new List<string>();
    }
}
