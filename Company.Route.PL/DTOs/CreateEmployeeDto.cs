using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Route.PL.DTOs
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Name is Required !!")]
        public string Name { get; set; }
        [Range(22, 60, ErrorMessage = "The Age Must be Betwean 22 to 60")]
        public double Age { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not Valid !!")]
        public string Email { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{3,10}-[a-zA-Z]{3,10}-[a-zA-Z]{3,10}"
            , ErrorMessage = "Address must be like 123-street-city-country")]
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }

        [DisplayName("DataType Of Create")]
        public DateTime CreateAt { get; set; }


        [DisplayName("Department")]
        public int? DepartmentId { get; set; }


    }
}
