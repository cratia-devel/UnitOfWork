using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DAL
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            using (var db = new SystemContext())
            {
                try
                {
                    Random rnd = new Random();

                    Partner _partner1 = new Partner
                    {
                        Person = db.Persons.First(x => x.LastName == "Camacho"),
                        Percentage = 0.25,
                    };

                    Partner _partner2 = new Partner
                    {
                        Person = db.Persons.First(x => x.LastName == "Ratia"),
                        Percentage = 0.25,
                    };

                    Waste _wasted = new Waste
                    {
                        DateTime = DateTime.Now,
                        WasteType = db.TypesOfWaste.Find(rnd.Next(1, 3)),
                        Weight = Math.Round((double)rnd.Next(1, 10000) / (double)rnd.Next(1, 100), 2),
                        Cost = Math.Round((double)rnd.Next(1, 10000) / (double)rnd.Next(1, 100), 2),
                        SalePrice = 2.0 * Math.Round((double)rnd.Next(1, 10000) / (double)rnd.Next(1, 100), 2),
                    };

                    _wasted.AddPartner(_partner1);
                    _wasted.AddPartner(_partner2);
                    _wasted.AddPartner(_partner1);
                    _wasted.AddPartner(_partner2);

                    db.Wastes.Add(_wasted);

                    int count = db.SaveChanges();
                    Console.WriteLine("{0} records saved to database", count);
                    Console.WriteLine();

                    var data1 = db.Wastes
                        .Include(x => x.WasteType)
                        .Include(x => x.Partners)
                            .ThenInclude(p => p.Person)
                        .ToList();
                    foreach (var item in data1)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    var data2 = db.TypesOfWaste.ToList();
                    foreach (var item in data2)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    var data3 = db.Partners
                        .Include(p => p.Person)
                        .ToList();
                    foreach (var item in data3)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine();
                    var data4 = db.Persons.ToList();
                    foreach (var item in data4)
                    {
                        Console.WriteLine(item);
                    }
                    var _GainPerPartners = data1.Last().GainByPartner();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ERROR] -- Exception: " + ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
                }
                finally
                {
                    Console.ReadKey();
                }
            }
        }
    }
}

