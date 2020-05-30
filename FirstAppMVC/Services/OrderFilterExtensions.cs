using FirstAppMVC.DAL.Entities;
using FirstAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Services
{
    public static class OrderFilterExtensions
    {
        public static IEnumerable<Order> ByClientName(this IEnumerable<Order> productOrderModels,
            string clientName)
        {
            if (!string.IsNullOrWhiteSpace(clientName))
                return productOrderModels.Where(o => o.ClientName.Contains(clientName));
            return productOrderModels;
        }

        public static IEnumerable<Order> ByPriceFrom(this IEnumerable<Order> productOrderModels,
            decimal? priceFrom)
        {
            if (priceFrom.HasValue)
                return productOrderModels.Where(o => o.Amount >= priceFrom);
            return productOrderModels;
        }

        public static IEnumerable<Order> ByPriceTo(this IEnumerable<Order> productOrderModels,
            decimal? priceTo)
        {
            if (priceTo.HasValue)
                return productOrderModels.Where(o => o.Amount <= priceTo);
            return productOrderModels;
        }
    }
}
