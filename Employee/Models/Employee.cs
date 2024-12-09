namespace Employee.Models
{
    public class Employee
    { 
        public int id { get; set; }
        public required string name { get; set; }
        public string? email { get; set; }
        public required string phoneNo { get; set; }
        public decimal salary { get; set; }

    }
}
