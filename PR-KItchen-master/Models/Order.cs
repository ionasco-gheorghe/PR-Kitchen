using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Kitchen.Models
{
    public class Order
    {
        public long Id { get; set; }
        public List<long> Items { get; set; }
        public int Priority { get; set; }
        public float MaxWaitTime { get; set; }
        public long TableId { get; set; }
        public List<KitchenFood> RealItems { get; set; }
        public DateTime ReceivedAt { get; set; }

        public Order()
        {
            RealItems = new List<KitchenFood>();
            ReceivedAt = DateTime.Now;
        }

        public bool IsReady { 
            get {
                bool ready = true;
                RealItems.ForEach(kf => {
                    if (kf.State != KitchenFoodState.Ready) ready = false;
                });
                return ready;
            }
        }
    }
}
