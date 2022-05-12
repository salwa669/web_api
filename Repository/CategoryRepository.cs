using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Repository
{
    public class CategoryRepository: ICategoryRepository
    {

        Context context;
        public CategoryRepository(Context _context)
        {
            context = _context;
        }
        public List<Category> GetAll()
        {
            return context.Category.ToList();
        }
        public Category GetById(int id)
        {
            return context.Category.FirstOrDefault(x => x.Id == id);
        }
        public Category GetCategoryDetails(int id)
        {
            return context.Category.Include(c=>c.Products).FirstOrDefault(x => x.Id == id);
        }
        public int Insert(Category cat)
        {
            context.Category.Add(cat);
            return context.SaveChanges();
        }

        public int Update(int id, Category cat)
        {
            Category oldcat = GetById(id);
            if (oldcat != null)
            {
                oldcat.Name = cat.Name;

                return context.SaveChanges();
            }
            return 0;
        }

        public int Delete(int id)
        {
            Category oldcat = GetById(id);
            context.Category.Remove(oldcat);
            return context.SaveChanges();
        }


    }
}

