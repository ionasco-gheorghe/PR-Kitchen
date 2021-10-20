using Kitchen.Infrastructure.Utils;
using Kitchen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kitchen.Server
{
    public class KitchenServer
    {
        private static HttpListener listener;
        private static string receiveUrl = "http://localhost:8000/";
        private static string sendUrl = "http://localhost:8001/";

        private Kitchen kitchen;

        public async Task HandleIncomingConnections()
        {
            bool runServer = true;

            while (runServer)
            {
                HttpListenerContext ctx = await listener.GetContextAsync();
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown"))
                {
                    Console.WriteLine("Shutdown requested");
                    runServer = false;
                }

                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/order"))
                {
                    using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
                    {
                        string data = reader.ReadToEnd();
                        Order order = JsonSerializer.Deserialize<Order>(data);
                        Logger.Log($"Received new order. Order {order.Id}");
                        kitchen.AddOrder(order);
                    }
                }

                resp.StatusCode = 200;
                resp.Close();
            }
        }

        public void SendReadyOrder(Order order)
        {
            using (var client = new HttpClient())
            {
                var message = JsonSerializer.Serialize(order);
                var response = client.PostAsync(sendUrl + "ready", new StringContent(message, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    Logger.Log($"Order {order.Id} is sent back");
                    kitchen.orders.Remove(order);
                }
                else
                    Logger.Log("Error sending ready order.");
            }
        }


        public async Task StartAsync(Kitchen kitchen)
        {
            this.kitchen = kitchen;
            
            listener = new HttpListener();
            listener.Prefixes.Add(receiveUrl);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", receiveUrl);

            // Handle requests
            await HandleIncomingConnections();

            // Close the listener
            listener.Close();
        }
    }
}
