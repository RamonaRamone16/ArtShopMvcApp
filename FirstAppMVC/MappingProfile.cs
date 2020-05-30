using AutoMapper;
using FirstAppMVC.DAL.Entities;
using FirstAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ProductToProductModelMap();
            ProductCreateModelToProductMap();
            CategoryToCategoryModelMap();
            CategoryCreateModelToCategoryMap();
            BrandToBrandModelMap();
            BrandCreateModelToBrandMap();
            ProductToProductEditModelMap();
            ProductEditModelToProductMap();
            ProductToProductOrderModelMap();
            ProductOrderModelToOrderMap();
            OrderToProductOrderModelMap();
        }
        public void ProductToProductModelMap()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(to => to.Brand, from => from.MapFrom
                (p => p.BrandId == null ? ProductModel.NoBrand : p.Brand.Name))
                .ForMember(to => to.Category, from => from.MapFrom(p => p.Category.Name))
                .ForMember(to => to.OrdersCount, from => from.MapFrom(p => p.Orders.Count));
        }
        public void ProductCreateModelToProductMap()
        {
            CreateMap<ProductCreateModel, Product>();
        }

        public void CategoryToCategoryModelMap()
        {
            CreateMap<Category, CategoryModel>();
        }

        public void CategoryCreateModelToCategoryMap()
        {
            CreateMap<CategoryCreateModel, Category>();
        }

        public void BrandToBrandModelMap()
        {
            CreateMap<Brand, BrandModel>();
        }
        public void BrandCreateModelToBrandMap()
        {
            CreateMap<BrandCreateModel, Brand>();
        }

        public void ProductToProductEditModelMap()
        {
            CreateMap<Product, ProductEditModel>();
        }
        public void ProductEditModelToProductMap()
        {
            CreateMap<ProductEditModel, Product>();
        }

        public void ProductToProductOrderModelMap()
        {
            CreateMap<Product, ProductOrderModel>()
                .ForMember(to => to.ProductId, from => from.MapFrom(p => p.Id))
                .ForMember(to => to.ProductName, from => from.MapFrom(p => p.Name))
                .ForMember(to => to.Brand, from => from.MapFrom(p => p.Brand.Name))
                .ForMember(to => to.ProductPrice, from => from.MapFrom(p => p.Price));
        }

        public void ProductOrderModelToOrderMap()
        {
            CreateMap<ProductOrderModel, Order>();
        }

        public void OrderToProductOrderModelMap()
        {
            CreateMap<Order, ProductOrderModel>()
                .ForMember(to => to.ProductName, from => from.MapFrom(p => p.Product.Name))
                .ForMember(to => to.Brand, from => from.MapFrom(p => p.Product.Brand.Name))
                .ForMember(to => to.ProductPrice, from => from.MapFrom(p => p.Product.Price));
        }
    }
}
