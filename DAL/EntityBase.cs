using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Updated_At { get; set; } = DateTime.MinValue;
        public DateTime Deleted_At { get; set; } = DateTime.MinValue;
    }
}
