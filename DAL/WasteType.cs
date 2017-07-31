using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class WasteType : EntityBase
    {
        [Required]
        public string Description { get; set; }
        public ICollection<Waste> Wastes { get; set; }

        public override string ToString()
        {
            string render = string.Empty;
            render += "[" + this.Id.ToString() + "] - ";
            render += this.Description;
            render += "\t{" + this.Created_At.ToString();
            render += " | " + this.Updated_At.ToString() + "}";
            return render;
        }
    }
}
