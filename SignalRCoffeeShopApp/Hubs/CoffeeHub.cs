using Microsoft.AspNetCore.SignalR;
using SignalRCoffeeShopApp.Models;
using SignalRCoffeeShopApp.Services;

namespace SignalRCoffeeShopApp.Hubs
{
    public class CoffeeHub : Hub
    {
        private readonly OrderService orderService;

        public CoffeeHub(OrderService _orderService)
        {
            orderService = _orderService;
        }

        public async Task GetUpdateForOrder(int orderId)
        {
            CheckResult result;

            do
            {
                result = orderService.GetUpdate(orderId);

                if (result.New)
                {
                    await Clients.Caller.SendAsync("ReceiveOrderUpdate", result.Update);
                }
            } 
            while (!result.Finished);

            await Clients.Caller.SendAsync("Finished");
        }
    }
}
