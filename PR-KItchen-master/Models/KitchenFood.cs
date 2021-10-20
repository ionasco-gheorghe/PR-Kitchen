using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen.Models
{
    public class KitchenFood : Food
    {
        public KitchenFoodState State;

        public KitchenFood(Food food)
        {
            this.Name = food.Name;
            this.PreparitionTime = food.PreparitionTime;
            this.CookingApparatus = food.CookingApparatus;
            this.Comlexity = food.Comlexity;
            State = KitchenFoodState.NotStarted;
        } 
    }


    public enum KitchenFoodState
    {
        NotStarted,
        Preparing,
        Ready
    }
}
