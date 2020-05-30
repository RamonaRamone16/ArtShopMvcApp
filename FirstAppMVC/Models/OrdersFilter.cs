using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Models
{
    public class OrdersFilter
    {
        public string ClientName { get; set; }

        [Display(Name = "Price From")]
        public decimal? PriceFrom { get; set; }

        [Display(Name = "Price From")]
        public decimal? PriceTo { get; set; }
        public List<ProductOrderModel> Orders { get; set; }
    }
}
