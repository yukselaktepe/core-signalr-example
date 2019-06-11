using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalrService
{
    class Program
    {
        static HubConnection connectionSignalR;
        static void Main(string[] args)
        {
            Connect().Wait();
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("> value<enter>");

            var cancelled = false;
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cancelled = true;
            };

            while (!cancelled)
            {
                Console.Write("> ");

                string text;
                try
                {
                    text = Console.ReadLine();
                }
                catch (Exception)
                {
                    break;
                }
                if (text == null)
                {
                    break;
                }

                try
                {
                    connectionSignalR.InvokeAsync("PushNotify", text);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"failed to deliver message: {e.Message}");
                }
            }
        }

        public static async Task Connect()
        {
            connectionSignalR = new HubConnectionBuilder()
               .WithUrl("http://localhost:5555/ExampleHub")
               .Build();
            await connectionSignalR.StartAsync();
        }
    }
}