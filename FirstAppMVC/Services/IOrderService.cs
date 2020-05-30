using FirstAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Services
{
    public interface IOrderService
    {
        ProductOrderModel GetProductOrderModel(int id);
        void CreateOrder(ProductOrderModel order);
        List<ProductOrderModel> SearchProductOrderModels(OrdersFilter ordersFilter);
    }
}
