using Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kitchen.Infrastructure.Seeding
{
    public class Seeding
    {
        public static void SeedMenu(List<Food> menu)
        {
            menu.AddRange(new Food[]
            {
                new Food
                {
                    Id = 1,
                    Name = "Pizza",
                    PreparitionTime = 20,
                    Comlexity = 2,
                    CookingApparatus = CookingApparatusType.Oven
                },
                new Food
                {
                    Id = 2,
                    Name = "Salad",
                    PreparitionTime = 10,
                    Comlexity = 1,
                    CookingApparatus = null
                },
                new Food
                {
                    Id = 3,
                    Name = "Zeama",
                    PreparitionTime = 7,
                    Comlexity = 1,
                    CookingApparatus = CookingApparatusType.Stove
                },
                new Food
                {
                    Id = 4,
                    Name = "Scallop Sashami with Meyer Lemon Confit",
                    PreparitionTime = 32,
                    Comlexity = 3,
                    CookingApparatus = null
                },
                new Food
                {
                    Id = 5,
                    Name = "Island Duck with Mulberry Mustard",
                    PreparitionTime = 35,
                    Comlexity = 3,
                    CookingApparatus = CookingApparatusType.Oven
                },
                new Food
                {
                    Id = 6,
                    Name = "Waffles",
                    PreparitionTime = 10,
                    Comlexity = 1,
                    CookingApparatus = CookingApparatusType.Stove
                },
                new Food
                {
                    Id = 7,
                    Name = "Aubergine",
                    PreparitionTime = 20,
                    Comlexity = 2,
                    CookingApparatus = null
                },
                new Food
                {
                    Id = 8,
                    Name = "Lasagna",
                    PreparitionTime = 30,
                    Comlexity = 2,
                    CookingApparatus = CookingApparatusType.Oven
                },
                new Food
                {
                    Id = 9,
                    Name = "Burger",
                    PreparitionTime = 15,
                    Comlexity = 1,
                    CookingApparatus = CookingApparatusType.Oven
                },
                new Food
                {
                    Id = 10,
                    Name = "Gyros",
                    PreparitionTime = 15,
                    Comlexity = 1,
                    CookingApparatus = null
                }
            });
        }

        public static void SeedCookingApparatus(List<CookingApparatus> apparatuses)
        {
            apparatuses.AddRange(new CookingApparatus[]
            {
                new CookingApparatus { Type = CookingApparatusType.Oven },
                new CookingApparatus { Type = CookingApparatusType.Oven },
                new CookingApparatus { Type = CookingApparatusType.Stove },
            });
        }

        public static void SeedCooks(List<Cook> cooks)
        {
            cooks.AddRange(new Cook[]
            {
                new Cook
                {
                    Rank = 1,
                    Proficiency = 1,
                    Name = "Larry Page",
                    CatchPhrase = "-"
                },
                new Cook
                {
                    Rank = 2,
                    Proficiency = 3,
                    Name = "John Doe",
                    CatchPhrase = ";D"
                },
                new Cook
                {
                    Rank = 3,
                    Proficiency = 4,
                    Name = "Homer Simpson",
                    CatchPhrase = "Beer!!"
                },
            });

            cooks.Sort((o1, o2) => o1.Proficiency - o2.Proficiency);
        }

    }
}
