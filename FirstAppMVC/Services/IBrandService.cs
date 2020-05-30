using FirstAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Services
{
    public interface IBrandService
    {
        List<BrandModel> SearchBrand(BrandModel brandModel);
        void CreateBrand(BrandCreateModel brandModel);
    }
}
