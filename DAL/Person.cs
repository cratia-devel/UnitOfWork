using System.Collections.Generic;

namespace DAL.Models
{
    public class Person : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Partner> Business { get; set; }
        public override string ToString()
        {
            string render = string.Empty;
            render += "[" + this.Id.ToString() + "] - ";
            render += this.FirstName + " ";
            render += this.LastName + " ";
            render += "\t{" + this.Created_At.ToString();
            render += " | " + this.Updated_At.ToString() + "}";
            return render;
        }
        public string FullName()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
