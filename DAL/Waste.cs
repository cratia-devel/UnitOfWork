using System;
using System.Collections.Generic;
using System.Globalization;

namespace DAL.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Waste : EntityBase
    {
        public DateTime DateTime { get; set; } = DateTime.Now;
        public WasteType WasteType { get; set; }
        public double Weight { get; set; } = 0.0;
        public double Cost { get; set; } = 0.0;
        public double SalePrice { get; set; } = 0.0;
        public HashSet<Partner> Partners { get; private set; }
        public override string ToString()
        {
            string render = string.Empty;
            render += "[" + this.Id.ToString() + "] - ";
            render += this.WasteType.Description + " ";
            render += this.Weight.ToString("N2") + " Kg ";
            render += this.Cost.ToString("C2") + " ";
            render += this.SalePrice.ToString("C2") + " ";
            render += "[" + this.DateTime.ToString("dd/MM/yyyy") + "]";
            render += "(" + this.DateTime.DayOfWeek.ToString() + ")";
            render += "(" + CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(this.DateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) + ")";
            if (this.Partners != null)
            {
                foreach (var item in this.Partners)
                {
                    render += "\n\t [ " + item.Percentage.ToString("P2") + " ] => " + item.Person.FullName();
                }
            }
            //render += "\t{" + this.Created_At.ToString();
            //render += " | " + this.Updated_At.ToString() + "}";
            return render;
        }
        public List<Object> GainByPartner()
        {
            if (this.Partners == null)
                return null;
            var _GainByPartner = new List<Object>();
            foreach (var item in Partners)
            {
                _GainByPartner.Add(new { PersonID = item.Person.Id, Gain = Math.Round(item.Percentage * this.Gain(), 2) });
            }
            return _GainByPartner;
        }
        public double Gain()
        {
            return Math.Round(this.SalePrice * this.Weight, 2);
        }
        public bool AddPartner(Partner _partner)
        {
            if (_partner == null)
                return false;
            else
            {
                if (this.Partners == null)
                {
                    this.Partners = new HashSet<Partner>();
                }
                return (this.Partners.Add(_partner));
            }
        }
    }
}
