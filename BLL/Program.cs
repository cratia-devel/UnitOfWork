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
            using (var repo = new Repository<Person>())
            {

                Random rnd = new Random();
                List<Person> data = new List<Person>();
                data.Add(new Person
                {
                    FirstName = "Carlos",
                    LastName = "Ratia",
                });
                data.Add(new Person
                {
                    FirstName = "Ignacio",
                    LastName = "Ratia",
                });
                data.Add(new Person
                {
                    FirstName = "Ignacio",
                    LastName = "Ratia",
                });

                repo.Add(data);

                if (repo.Delete(x => x.LastName == "Ratia"))
                    Console.WriteLine("Eliminado Persona");
                if (repo.Delete(3))
                    Console.WriteLine("Eliminado Persona");
                Console.WriteLine("Data en Tabla Personas");
                var data1 = repo.GetAll();
                foreach (var item in data1)
                {
                    Console.WriteLine(item);
                }

            }
            Console.ReadKey();
        }
    }
}