using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalrClient.Models;

namespace SignalrClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public class ExamplenHub : Hub
        {
            public override Task OnConnectedAsync()
            {
                return Clients.Client(Context.ConnectionId).SendAsync("SetConnectionId", Context.ConnectionId);
            }
            public async Task<string> ConnectGroup(string stocName, string connectionID)
            {
                await Groups.AddToGroupAsync(connectionID, stocName);
                return $"{connectionID} is added {stocName}";
            }
            public Task PushNotify(string text)
            {
                return Clients.Group("ExampleGroup").SendAsync("ChangeProductValue", text);
            }
        }
    }
}
