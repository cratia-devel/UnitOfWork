using BLL.Repository;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var uow = new UnitOfWork<SystemContext>())
            {
                Random rnd = new Random();
                uow.GetRepository<Waste>()
                    .Add(new Waste
                    {
                        DateTime = DateTime.Now,
                        WasteType = uow.GetRepository<WasteType>().Firts(),
                        Weight = Math.Round((double)rnd.Next(1, 10000) / (double)rnd.Next(1, 100), 2),
                        Cost = Math.Round((double)rnd.Next(1, 10000) / (double)rnd.Next(1, 100), 2),
                        Partners = new HashSet<Partner>
                            {
                                new Partner {
                                    Person = uow.GetRepository<Person>().Firts(),
                                    Percentage = 0.50,
                                },
                                new Partner {
                                    Person = uow.GetRepository<Person>().Last(),
                                    Percentage = 0.50,
                                },
                            },
                        SalePrice = 2.0 * Math.Round((double)rnd.Next(1, 10000) / (double)rnd.Next(1, 100), 2),
                    });
                if (uow.Commit() > 0)
                {
                    Console.WriteLine("<-------List Of Wasted------->");
                    foreach (var item in uow.GetRepository<Waste>().GetAll())
                    {
                        Console.WriteLine(item);
                    }
                }

                Console.WriteLine("<-------List Of Person------->");
                foreach (var item in uow.GetRepository<Person>().Get(null, null, "Business"))
                {
                    Console.WriteLine(item);
                }
                Console.ReadKey();
            }            
        }        
    }
}
