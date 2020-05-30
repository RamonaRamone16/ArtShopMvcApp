using AutoMapper;
using FirstAppMVC.DAL;
using FirstAppMVC.DAL.Entities;
using FirstAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public OrderService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            if (unitOfWorkFactory == null)
                throw new ArgumentNullException(nameof(unitOfWorkFactory));
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public ProductOrderModel GetProductOrderModel(int id)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Product product = unitOfWork.Products.GetAllWithBrandsCategoriesAndOrders()
                    .FirstOrDefault(p => p.Id == id);
                return Mapper.Map<ProductOrderModel>(product);
            }
        }

        public void CreateOrder(ProductOrderModel productOrderModel)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                Product product = unitOfWork.Products.GetById(productOrderModel.ProductId);
                Order order = Mapper.Map<Order>(productOrderModel);
                order.Data = DateTime.Now;
                order.Amount = product.Price * order.Count;

                unitOfWork.Orders.Create(order);
            }
        }

        public List<ProductOrderModel> SearchProductOrderModels(OrdersFilter ordersFilter)
        {
            using (UnitOfWork unitOfWork = _unitOfWorkFactory.Create())
            {
                IEnumerable<Order> orders = unitOfWork.Orders.GetAllWithProducts()
                    .ByClientName(ordersFilter.ClientName)
                    .ByPriceFrom(ordersFilter.PriceFrom)
                    .ByPriceTo(ordersFilter.PriceTo);

                return Mapper.Map<List<ProductOrderModel>>(orders);
            }
        }
    }
}
