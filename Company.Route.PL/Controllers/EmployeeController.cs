using Company.Route.BLL.Interfaces;
using Company.Route.DAL;
using Company.Route.PL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Company.Route.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: EmployeeController
        public IActionResult Index()
        {
            var employee = _employeeRepository.GetAll();
            return View(employee);

        }


        // GET: EmployeeController/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: EmployeeController/Create
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {

                var employee = new Employee()
                {
                    Name = model.Name,
                    Age = model.Age,
                    Email = model.Email,
                    Address = model.Address,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    HiringDate = model.HiringDate,
                    CreateAt = model.CreateAt
                };

                var count = _employeeRepository.ADD(employee);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);

        }

        // GET: EmployeeController/Details/5
        public IActionResult Details(int id, string viewName = "Details")
        {
            if (id == null) return BadRequest("InNalid Id");
            var employee = _employeeRepository.Get(id);

            if (employee == null) return NotFound(new
            {
                StateCode = 404,
                Message = $"Department With {id}, Not Found"
            });

            return View(viewName, employee);
        }


        // GET: EmployeeController/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null) return BadRequest("InValid Id");
            var employee = _employeeRepository.Get(id);

            if (employee == null) return NotFound(new
            {
                StateCode = 404,
                Message = $"Department With {id}, Not Found"
            });
            var employeeDto = new CreateEmployeeDto()
            {
                Name = employee.Name,
                Age = employee.Age,
                Email = employee.Email,
                Address = employee.Address,
                Phone = employee.Phone,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                IsDeleted = employee.IsDeleted,
                HiringDate = employee.HiringDate,
                CreateAt = employee.CreateAt
            };

            return View(employeeDto);

            //Refactoring
            //return Details(id, "Edit");
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Id = id,
                    Name = model.Name,
                    Age = model.Age,
                    Email = model.Email,
                    Address = model.Address,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    HiringDate = model.HiringDate,
                    CreateAt = model.CreateAt
                };

                var count = _employeeRepository.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        // GET: EmployeeController/Delete/5
        public IActionResult Delete(int id)
        {
            //Refactoring
            return Details(id, "Delete");
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest(); // If You Fetch Id From Form Or Any Thing Except  Segment=Route return BadRequest:400 

                var count = _employeeRepository.Delete(employee);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(employee);
        }
    }
}
