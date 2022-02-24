using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace spp2
{
    class Program
    {

        static void Main(string[] args)
        {
            using RaceContext context = new RaceContext();


            Console.WriteLine("\"SELECT\" Races:");
            var race = context.Races
                .Include(r => r.Location)
                .ToList();
            foreach (var race1 in race)
            {
                Console.WriteLine($"Name: {race1.Name}\n" +
                                  $"Location: {race1.Location.Name}\n" +
                                  $"Country: {race1.Location.Country}");
            }
            Console.WriteLine("\"SELECT ORDER BY\" Races:");
            var racers = context.Racers
                .Include(r => r.Team)
                .Include(r => r.Car)
                .OrderBy(r => r.Team.Id)
                .ToList();
            foreach (var racer1 in racers)
            {
                Console.WriteLine($"Team: {racer1.Team.Name}\n" +
                                  $"FIO: {racer1.Name}\n" +
                                  $"Car: {racer1.Car.Mark}");
            }


            Console.WriteLine("\n\"INSERT\" Cars:");
            Console.WriteLine("\"INSERT\" Before:");
            foreach (var car in context.Cars.ToList())
            {
                Console.WriteLine($"Mark: {car.Mark}");
            }
            context.Cars.AddRange(
                new Car() {Mark = "Mark4"},
                new Car() {Mark = "Mark5"});
            context.SaveChanges();
            Console.WriteLine("\"INSERT\" After:");
            foreach (var car in context.Cars.ToList())
            {
                Console.WriteLine($"Mark: {car.Mark}");
            }


            Console.WriteLine("\n\"DELETE\" Cars:");
            Console.WriteLine("\"DELETE\" Before:");
            foreach (var car in context.Cars.ToList())
            {
                Console.WriteLine($"Mark: {car.Mark}");
            }

            var delete = context.Cars.Where(c => c.Mark == "Mark4" || c.Mark == "Mark5");
            context.Cars.RemoveRange(delete);
            context.SaveChanges();
            Console.WriteLine("\"DELETE\" After:");
            foreach (var car in context.Cars.ToList())
            {
                Console.WriteLine($"Mark: {car.Mark}");
            }


            Console.WriteLine("\n\"UPADATE\" Before:");
            var update1 = context.Teams.First(t => t.Id == 1);
            Console.WriteLine($"Name: {update1.Name}");
            Console.WriteLine("\"UPADATE\" After:");
            update1.Name = "Team3";
            context.Teams.Update(update1);
            context.SaveChanges();
            update1 = context.Teams.First(t => t.Id == 1);
            Console.WriteLine($"Name: {update1.Name}");
        }
    }
}
