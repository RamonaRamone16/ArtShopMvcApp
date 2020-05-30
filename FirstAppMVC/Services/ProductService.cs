using FirstAppMVC.Models;
using System;
using System.Collections.Generic;
using FirstAppMVC.DAL;
using System.Linq;
using System.Threading.Tasks;
using FirstAppMVC.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstAppMVC.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ProductService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public List<ProductModel> SearchProducts(ProductFilter model)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                IEnumerable<Product> products = unitOfWork.Products.GetAllWithBrandsCategoriesAndOrders()
                    .ByPriceFrom(model.PriceFrom)
                    .ByPriceTo(model.PriceTo)
                    .ByName(model.Name)
                    .ByCategoryId(unitOfWork, model.CategoryId)
                    .ByBrandId(unitOfWork, model.BrandId);

                return Mapper.Map<List<ProductModel>>(products);
            }
        }
        public ProductCreateModel GetProductCreateModel()
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                return new ProductCreateModel()
                {
                    CategorySelect = GetSelectListCategories(),
                    BrandSelect = GetSelectListBrands()
                };
            }   
        }

        public void CreateProduct(ProductCreateModel model)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                unitOfWork.Products.Create(Mapper.Map<Product>(model));
            }
        }

        public SelectList GetSelectListCategories()
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                List<Category> categories = unitOfWork.Categories.GetAll().ToList();
                return new SelectList(categories, nameof(Category.Id), nameof(Category.Name));
            }
        }

        public SelectList GetSelectListBrands()
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                List<Brand> brands = unitOfWork.Brands.GetAll().ToList();
                return new SelectList(brands, nameof(Brand.Id), nameof(Brand.Name));
            }
        }

        public ProductEditModel GetProductEditModel(int id)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Product product = unitOfWork.Products.GetById(id);
                ProductEditModel productEditModel = Mapper.Map<ProductEditModel>(product);

                productEditModel.Brands = new SelectList(unitOfWork.Brands.GetAll(),
                    nameof(Brand.Id), nameof(Brand.Name), productEditModel.BrandId);

                productEditModel.Categories = new SelectList(unitOfWork.Categories.GetAll(),
                    nameof(Category.Id), nameof(Category.Name), productEditModel.CategoryId);

                return productEditModel;
            }
        }

        public void UpdateProduct(ProductEditModel productEditModel)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                unitOfWork.Products.Update(Mapper.Map<Product>(productEditModel));
            }
        }
    }
}
