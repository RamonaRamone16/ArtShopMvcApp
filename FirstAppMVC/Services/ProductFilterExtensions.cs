using FirstAppMVC.DAL;
using FirstAppMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstAppMVC.Services
{

    public static class ProductFilterExtensions
    {
        public static IEnumerable<Product> ByPriceFrom(this IEnumerable<Product> products, decimal? priceFrom)
        {
            if (priceFrom.HasValue)
                return products.Where(p => p.Price >= priceFrom.Value);
            return products;
        }

        public static IEnumerable<Product> ByPriceTo(this IEnumerable<Product> products, decimal? priceTo)
        {
            if (priceTo.HasValue)
                return products.Where(p => p.Price <= priceTo.Value);
            return products;
        }

        public static IEnumerable<Product> ByName(this IEnumerable<Product> products, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                return products.Where(p => p.Name.Contains(name));
            return products;
        }

        public static IEnumerable<Product> ByCategoryId(this IEnumerable<Product> products,
            UnitOfWork unitOfWork, int? categoryId)
        {
            if (categoryId.HasValue)
            {
                var category = unitOfWork.Categories.GetById(categoryId.Value);
                if (category == null)
                    throw new ArgumentOutOfRangeException(nameof(category),
                        $"No category with Id {categoryId}");
                return products.Where(p => p.CategoryId == categoryId.Value);
            }

            return products;
        }
        public static IEnumerable<Product> ByBrandId(this IEnumerable<Product> products,
            UnitOfWork unitOfWork, int? brandId)
        {
            if (brandId.HasValue)
            {
                var category = unitOfWork.Brands.GetById(brandId.Value);
                if (category == null)
                    throw new ArgumentOutOfRangeException(nameof(category),
                        $"No category with Id {brandId}");
                return products.Where(p => p.BrandId == brandId.Value);
            }

            return products;
        }
    }
}
