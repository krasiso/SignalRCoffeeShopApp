using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRCoffeeShopApp.Hubs;
using SignalRCoffeeShopApp.Models;
using SignalRCoffeeShopApp.Services;

namespace SignalRCoffeeShopApp.Controllers
{
    public class CoffeeController : Controller
    {
        private readonly OrderService orderService;

        private readonly IHubContext<CoffeeHub> coffeeHub;

        public CoffeeController(
            OrderService _orderService,
            IHubContext<CoffeeHub> _hubContext)
        {
            orderService = _orderService;
            coffeeHub = _hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> OrderCoffee([FromBody] Order order)
        {
            await coffeeHub.Clients.All.SendAsync("NewOrder", order);
            var orderId = orderService.NewOrder();

            return Accepted(orderId);
        }
    }
}
