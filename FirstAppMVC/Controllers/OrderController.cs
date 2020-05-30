using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstAppMVC.Models;
using FirstAppMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstAppMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            if (orderService == null)
                throw new ArgumentNullException(nameof(orderService));
            _orderService = orderService;
        }


        [HttpGet]
        public IActionResult Index(OrdersFilter ordersFilter)
        {
            try
            {
                List<ProductOrderModel> orders = _orderService
                    .SearchProductOrderModels(ordersFilter);
                ordersFilter.Orders = orders;

                return View(ordersFilter);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("Order/{id}")]
        public IActionResult Order(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.BadRequestMessage = "Product Id is Null";
                return View("BadRequest");
            }
            ProductOrderModel productOrderModel = _orderService.GetProductOrderModel(id.Value);
            return View(productOrderModel);
        }


        [HttpPost]
        public IActionResult OrderAnother(ProductOrderModel order)
        {
            _orderService.CreateOrder(order);
            return RedirectToAction("Index");
        }
    }
}