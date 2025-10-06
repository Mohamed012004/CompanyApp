
namespace Company.Route.DAL.Models
{
    public class Department : BaseEntity
    {

        public string Code { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }



        // Navigation Property
        public List<Employee>? Employees { get; set; }

    }
}
