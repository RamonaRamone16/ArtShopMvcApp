using FirstAppMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Services
{
    public static class BrandFilterExtensions
    {
        public static IEnumerable<Brand> ByName(this IEnumerable<Brand> brands, string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
                return brands.Where(b => b.Name.Contains(name));
            return brands;
        }
    }
}
