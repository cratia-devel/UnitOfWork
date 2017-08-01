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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            using (var uow = new UnitOfWork<SystemContext>())
            {
                Random rnd = new Random();
                uow.GetRepository<Person>()
                    .Add(new Person
                    {
                        FirstName = "Sofia",
                        LastName = "Ratia",
                    });
                uow.GetRepository<Person>()
                    .Add(new Person
                    {
                        FirstName = "Sofia",
                        LastName = "Ratia",
                    });
                uow.GetRepository<Person>()
                    .Add(new Person
                    {
                        FirstName = "Camila",
                        LastName = "Canaval",
                    });
                uow.GetRepository<WasteType>()
                    .Add(
                        new WasteType
                        {
                            Description = "DESPERDICIO POLLO",
                        }
                    );
                if (uow.Commit() > 0)
                    Console.WriteLine("Successfully saved data.......!!!!!");

                WasteType dataupdate = uow
                    .GetRepository<WasteType>()
                    .Find(wt => wt.Description == "DESPERDICIO BOVINO");
                if (dataupdate != null)
                {
                    dataupdate.Description = "_DESPERDICIO_BOVINO_";
                    uow.GetRepository<WasteType>()
                        .Update(dataupdate);
                    if (uow.Commit() > 0)
                        Console.WriteLine("Successfully updated data.......!!!!!");
                }
            }
            Console.ReadKey();
        }
    }
}