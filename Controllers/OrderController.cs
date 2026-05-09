using Microsoft.AspNetCore.Mvc;
using BookstoreApp.Models;
using BookstoreApp.Extensions;
using BookstoreApp.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderController(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [Route("Order/Checkout")]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            if (!cart.Any())
            {
                return RedirectToPage("/Cart/Index");
            }

            var totalAmount = cart.Sum(item => item.Book.Price * item.Quantity);
            ViewBag.TotalAmount = totalAmount;

            return View(cart);
        }

        [HttpPost]
        [Route("Order/ProcessOrder")]
        public async Task<IActionResult> ProcessOrder()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToPage("/Account/Login");
            }

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            if (!cart.Any())
            {
                return RedirectToPage("/Cart/Index");
            }

            var order = new Order
            {
                UserId = 1, // Mock user id
                OrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                TotalAmount = cart.Sum(item => item.Book.Price * item.Quantity),
                OrderItems = cart
            };

            await _orderRepository.AddAsync(order);
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Confirmation", new { orderId = order.Id });
        }

        [Route("Order/Confirmation/{orderId:int}")]
        public async Task<IActionResult> Confirmation(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
