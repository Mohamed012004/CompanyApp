namespace Company.Route.DAL
{
    public class Employee
    {
        public int Id { get; set; }
        public double Age { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime HiringDate { get; set; }
        public DateTime CreateAt { get; set; }


    }
}
