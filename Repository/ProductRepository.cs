using Ecommerce.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ecommerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        Context context;
     
        public ProductRepository(Context _context)
        {
            context = _context;
         
        }
        public List<Product> GetAll()
        {
            return context.Product.ToList();
        }
        public Product GetById(int id)
        {
            return context.Product.FirstOrDefault(x => x.Id == id);
        }
        public int Insert(Product product)
        {
            context.Product.Add(product);
            return context.SaveChanges();
        }

        public int Update(int id, Product product)
        {
            Product oldprod = GetById(id);
            if (oldprod != null)
            {
                
                oldprod.Name = product.Name;
                oldprod.Quantity = product.Quantity;
                oldprod.Price = product.Price;
                oldprod.Image = product.Image;
                oldprod.CategoryId = product.CategoryId;

                return context.SaveChanges();
            }
            return 0;
        }

        public int Delete(int id)
        {
            Product oldprod = GetById(id);
            context.Product.Remove(oldprod);
            return context.SaveChanges();
        }
    }

   
}
