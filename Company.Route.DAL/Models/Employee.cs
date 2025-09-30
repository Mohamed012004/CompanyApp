using System.ComponentModel;

namespace Company.Route.DAL.Models
{
    public class Employee : BaseEntity
    {
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

        // F.K
        [DisplayName("Department")]
        public int? DepartmentID { get; set; }

        // Navigation Property
        public Department? Department { get; set; }


    }
}
