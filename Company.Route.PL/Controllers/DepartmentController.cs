using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Models;
using Company.Route.PL.DTOs;
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model) // Form
        {
            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };

                var count = _departmentRepository.ADD(department);

                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }


        public IActionResult Details(int id)
        {
            if (id == null) return BadRequest("InValid Id");
            var department = _departmentRepository.Get(id);
            if (department is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = $"Department With {id}, Not Found"
                });
            }

            return View(department);
        }


    }
}
