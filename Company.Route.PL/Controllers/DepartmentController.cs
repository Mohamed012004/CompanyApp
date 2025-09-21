using Company.Route.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.Route.PL.Controllers
{
    // MVC Controller 
    public class DepartmentController : Controller
    {
        public readonly IDepartmentRepository _departmentRepository; // Null
        // ASK  From CLR To Create Objet From DepartmentRepository 
        public DepartmentController(IDepartmentRepository departmentrepositoy) // Recommended To make ctor Against Reference Not Concrate Class 
        {
            _departmentRepository = departmentrepositoy;
        }

        // /Department/Index
        public IActionResult Index()   // Note: Its Perfect To implement Function Against Interface(IActionResult) Not Concrate Class(ActionResult)
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }

    }
}
