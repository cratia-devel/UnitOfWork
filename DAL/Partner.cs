namespace DAL.Models
{
    public class Partner : EntityBase
    {
        public Person Person { get; set; }
        public double Percentage { get; set; }
        public override string ToString()
        {
            string render = string.Empty;
            render += "[" + this.Id.ToString() + "] - ";
            render += this.Person.FullName() + " ";
            render += this.Percentage.ToString("P2") + " ";
            //render += "\t{" + this.Created_At.ToString();
            //render += " | " + this.Updated_At.ToString() + "}";
            return render;
        }
    }
}
