using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcApplication.Models;

namespace MvcApplication.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LightingContext db)
        {
            Random random = new Random();

            db.Database.EnsureCreated();

            if (db.Masters.Any() || db.Calls.Any() || db.Lamps.Any())
            {
                return;
            }

            /*=================================================   Блок инициализации данных   =================================================*/

            int mastersNumber = 100;
            int lampsNumber = 100;
            int lanternNumber = 100;
            int callsNumber = 100;
            int streetsNumber = 100;
            int sectionsNumber = 100;
            int sectionsLightingsNumber = 100;

            string[] masterNames = { "Григорий", "Фёдор", "Пётр", "Андрей", "Николай", "Александр", "Алексей", "Вячеслав" };
            string[] masterSurnames = { "Трампов", "Линейкин", "Ручкин", "Карандашкин", "Стёркин", "Точилкин", "Пеналкин", "Ложкин" };
            string[] lampNames = { "Лампа накаливания", "Светодиоидная лампа", "Металогалогенная лампа", "Галогеновый светильник" };
            string[] lanternNames = { "IP44 Dante", "Oxford Maytoni", "Capella F/3", "Apus F", "Cassiopeya F/3", "Piatto Led 7W" };
            string[] lanternTypes = { "Уличный", "Парковый", "Уличный/парковый", "Уличный трёхрожковый", "Уличный настенный" };
            string[] streetNames = { "Садовая", "Ленина", "Фрунзе", "Гагарина", "Советская", "Богдана Хмельницкого" };

            int lampLife, lampPower, workTime, countOfHouses, sectionNumber;
            DateTime dateCall;

            /*=================================================   Блок заполнения таблиц   =================================================*/

            for (int i = 0; i < mastersNumber; i++)
            {
                StringBuilder name = new StringBuilder();
                name.Append(getRandomString(masterNames, random)).Append(" ").Append(getRandomString(masterSurnames, random));

                db.Masters.Add(new Master { MasterName = name.ToString() });
            }

            db.SaveChanges();

            for (int i = 0; i < lampsNumber; i++)
            {
                StringBuilder name = new StringBuilder();
                name.Append(getRandomString(lampNames, random));

                db.Lamps.Add(new Lamp { LampName = name.ToString(), LampPower = random.Next(5, 40), LampLife = random.Next(1000, 20000) });
            }

            db.SaveChanges();

            for (int i = 0; i < lanternNumber; i++)
            {
                StringBuilder name = new StringBuilder();
                StringBuilder type = new StringBuilder();
                name.Append(getRandomString(lanternNames, random));
                type.Append(getRandomString(lanternTypes, random));

                bool isOperable = random.Next(0, 2) == 0 ? false : true;

                db.Lanterns.Add(new Lantern { LanternName = name.ToString(), LanternType = type.ToString(), LampId = random.Next(1, lanternNumber), IsOperable = isOperable });
            }

            db.SaveChanges();
        }

        private static string getRandomString(string[] array, Random random)
        {
            return array[random.Next(0, array.Length)];
        }
    }
}
