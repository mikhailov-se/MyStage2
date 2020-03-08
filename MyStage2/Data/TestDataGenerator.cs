using Bogus;
using MyStage2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStage2.Data
{
    public static class TestDataGenerator
    {
       public static Faker faker = new Faker("ru");

        public static Announsment GenerateAnnounsment()
        {

            var announsment = new Models.Announsment
            {
                Number = faker.Random.Number(1,400),
                CreateDate = faker.Date.Soon(),
                Rating = faker.Random.Number(10),
                TextAnnounsment ="Скоро состоится хакатон",
              
                User =new User
                {
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName()
                }
                
            };

            return announsment;

        }

    }
}
