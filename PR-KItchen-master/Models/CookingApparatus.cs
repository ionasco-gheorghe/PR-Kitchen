using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Kitchen.Models
{
    public class CookingApparatus
    {
        public long Id;
        public CookingApparatusType Type;
        public bool Busy;

        private static ObjectIDGenerator idGenerator = new ObjectIDGenerator();

        public CookingApparatus()
        {
            Id = idGenerator.GetId(this, out bool firstTime);
            Busy = false;
        }

    }

    public enum CookingApparatusType
    {
        Oven,
        Stove
    }
}
