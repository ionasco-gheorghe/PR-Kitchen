using Kitchen.Infrastructure.Utils;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;

namespace Kitchen.Models
{
    public class Cook
    {
        public long Id;
        public int Rank;
        public int Proficiency;
        public string Name;
        public string CatchPhrase;


        private static ObjectIDGenerator idGenerator = new ObjectIDGenerator();
        private static Mutex mutex = new Mutex();

        public Cook()
        {
            Id = idGenerator.GetId(this, out bool firstTime);
        }


        public void StartWork(Kitchen kitchen)
        {
            for (int i = 0; i < Proficiency; i++)
            {
                Thread.Sleep(100);
                new Thread(() =>
                {
                    while (true)
                    {
                        foreach (var order in kitchen.orders.ToList())
                        {
                            if (!order.IsReady)
                            {
                                foreach (var food in order.RealItems)
                                {
                                    if (food.Comlexity <= Rank)
                                    {
                                        mutex.WaitOne();
                                        if (food.State == KitchenFoodState.NotStarted && TryGetApparatus(kitchen, food, out CookingApparatus apparatus))
                                        {
                                            if (apparatus != null)
                                                apparatus.Busy = true;
                                            food.State = KitchenFoodState.Preparing;
                                            mutex.ReleaseMutex();
                                            Prepare(food, apparatus);
                                            if (order.IsReady)
                                            {
                                                Logger.Log($"Order {order.Id} is ready");
                                                kitchen.server.SendReadyOrder(order);
                                            }
                                        }
                                        else mutex.ReleaseMutex();
                                    }
                                }
                            }
                        }
                    }
                }).Start();
            }
        }

        public void Prepare(KitchenFood food, CookingApparatus apparatus)
        {
            Logger.Log($"Cook {Id} started preparing food {food.Name} ({food.PreparitionTime})" + (apparatus != null ? $" using {apparatus.Type} {apparatus.Id}" : ""));
            Thread.Sleep(food.PreparitionTime * Values.TIME_UNIT);
            food.State = KitchenFoodState.Ready;
            Logger.Log($"Cook {Id} prepared food {food.Name}");
            if (apparatus != null)
                apparatus.Busy = false;
        }

        public bool TryGetApparatus(Kitchen kitchen, KitchenFood food, out CookingApparatus apparatus)
        {
            if (!food.CookingApparatus.HasValue)
            {
                apparatus = null;
                return true;
            }

            apparatus = kitchen.apparatuses.Where(a => a.Type == food.CookingApparatus).FirstOrDefault(a => !a.Busy);

            return apparatus != null;
        }
        
    }
}
