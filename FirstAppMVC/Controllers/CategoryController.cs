using FirstAppMVC.Models;
using FirstAppMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAppMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            if (categoryService == null)
                throw new ArgumentNullException(nameof(categoryService));
            _categoryService = categoryService;
        }

        public IActionResult Index(CategoryModel model)
        {
            try
            {
                List<CategoryModel> models = _categoryService.SearchCategories(model);
                return View(models);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateCategory(CategoryCreateModel model)
        {
            _categoryService.CreateCategory(model);
            return RedirectToAction("Index");
        }
    }
}
