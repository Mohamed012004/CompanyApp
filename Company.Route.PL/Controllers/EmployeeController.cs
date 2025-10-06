using AutoMapper;
using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Models;
using Company.Route.PL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Company.Route.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        // GET: EmployeeController
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _employeeRepository.GetAll();
            }
            else
            {
                employees = _employeeRepository.GetByName(SearchInput);
            }
            // Dectionary: 3 Properties
            // 1. ViewData : Transfer Extra Information Fro Controller(Action) To View
            ViewData["Message"] = "Hello World From ViewData";

            // 2. ViewBag : Transfer Extra Information Fro Controller(Action) To View
            ViewBag.Message = "Hello World from ViewBag";

            // 3. TempData

            return View(employees);

        }


        // GET: EmployeeController/Create
        public IActionResult Create()
        {
            //var departments = _departmentRepository.GetAll();
            //ViewData["departments"] = departments;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: EmployeeController/Create
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                //// Manual Mapping
                //var employee = new Employee()
                //{
                //    Name = model.Name,
                //    Age = model.Age,
                //    Email = model.Email,
                //    Address = model.Address,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    HiringDate = model.HiringDate,
                //    CreateAt = model.CreateAt,
                //    DepartmentID = model.DepartmentId
                //};

                // AutoMapper Mapping
                var employee = _mapper.Map<Employee>(model);
                var count = _employeeRepository.ADD(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee Is Created Successfully";
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

            //var departments = _departmentRepository.GetAll();
            //ViewData["departments"] = departments;

            //var employeeDto = new CreateEmployeeDto()
            //{
            //    Name = employee.Name,
            //    Age = employee.Age,
            //    Email = employee.Email,
            //    Address = employee.Address,
            //    Phone = employee.Phone,
            //    Salary = employee.Salary,
            //    IsActive = employee.IsActive,
            //    IsDeleted = employee.IsDeleted,
            //    HiringDate = employee.HiringDate,
            //    CreateAt = employee.CreateAt,
            //    DepartmentId = employee.DepartmentID


            //};

            // Auto Mapper
            var employeeDto = _mapper.Map<CreateEmployeeDto>(employee);
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
                //var employee = new Employee()
                //{
                //    Id = id,
                //    Name = model.EmpName,
                //    Age = model.Age,
                //    Email = model.Email,
                //    Address = model.Address,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    HiringDate = model.HiringDate,
                //    CreateAt = model.CreateAt,
                //    DepartmentID = model.DepartmentId
                //};
                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;
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
            if (id == null) return BadRequest("Invalid Id");
            var employee = _employeeRepository.Get(id);
            if (employee is null) return NotFound(
                new
                {
                    StatusCode = 404,
                    Message = $"Employee With {id} Not Found"
                });
            return View(employee);
            //Refactoring
            //return Details(id, "Delete");
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
