using Kitchen.Infrastructure.Seeding;
using Kitchen.Infrastructure.Utils;
using Kitchen.Models;
using Kitchen.Server;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen
{
    public class Kitchen : BackgroundService
    {
        public KitchenServer server;
        public List<Food> menu = new List<Food>();
        public List<Order> orders = new List<Order>();
        public List<CookingApparatus> apparatuses = new List<CookingApparatus>();
        public List<Cook> cooks = new List<Cook>();


        public Kitchen(KitchenServer server)
        {
            this.server = server;
            this.server.StartAsync(this);
            Seeding.SeedMenu(menu);
        }

        public void InitCookingApparatus()
        {
            Seeding.SeedCookingApparatus(apparatuses);
        }

        public void InitCooks()
        {
            Seeding.SeedCooks(cooks);

            foreach(var cook in cooks)
                cook.StartWork(this);
        }

        public void AddOrder(Order order)
        {
            order.Items.ForEach(foodId => order.RealItems.Add(new KitchenFood(menu.First(f => f.Id == foodId))));
            orders.Add(order);
            orders.Sort((o1, o2) => o1.Priority - o2.Priority);
            orders.Sort((o1, o2) => (int)(o1.ReceivedAt.Ticks - o2.ReceivedAt.Ticks));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            InitCookingApparatus();
            InitCooks();
            return Task.CompletedTask;
        }
    }
}
