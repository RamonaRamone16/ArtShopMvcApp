using FirstAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Services
{
    public interface ICategoryService
    {
        List<CategoryModel> SearchCategories(CategoryModel model);
        void CreateCategory(CategoryCreateModel model);
    }
}
