using Ecommerce.Models;
using System.Collections.Generic;

namespace Ecommerce.Repository
{
    public interface ICategoryRepository
    {
        int Delete(int id);
        List<Category> GetAll();
        Category GetById(int id);
        int Insert(Category cat);
        int Update(int id, Category cat);
        Category GetCategoryDetails(int catid);
    }
}