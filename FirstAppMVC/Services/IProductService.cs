using FirstAppMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Services
{
    public interface IProductService
    {
        List<ProductModel> SearchProducts(ProductFilter model);
        ProductCreateModel GetProductCreateModel();
        void CreateProduct(ProductCreateModel model);
        SelectList GetSelectListCategories();
        SelectList GetSelectListBrands();
        ProductEditModel GetProductEditModel(int id);
        void UpdateProduct(ProductEditModel product);
    }
}
