using AutoMapper;
using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Models;
using Company.Route.PL.DTOs;
using Company.Route.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Route.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: EmployeeController
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
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
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: EmployeeController/Create
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                #region Manual Mapping
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
                #endregion

                if (model.Image is not null)
                {
                    model.ImageName = DocumentSetting.UploadFile(model.Image, "Images");
                }

                // AutoMapper Mapping
                var employee = _mapper.Map<Employee>(model);
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var count = await _unitOfWork.ComplatedAsync();
                if (count > 0)
                {
                    TempData["Message"] = "Employee Is Created Successfully";
                    return RedirectToAction("Index");
                }
            }

            ViewData["departments"] = await _unitOfWork.DepartmentRepository.GetAllAsync();

            return View(model);

        }




        // GET: EmployeeController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;

            if (id == null) return BadRequest("InValid Id");
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id);

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
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                #region Manual Mapping
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
                #endregion

                if (model.ImageName is not null && model.Image is not null)
                {
                    DocumentSetting.Delete(model.ImageName, "Images");
                }

                if (model.Image is not null)
                {
                    model.ImageName = DocumentSetting.UploadFile(model.Image, "Images");
                }



                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;
                _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.ComplatedAsync();

                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewData["departments"] = await _unitOfWork.DepartmentRepository.GetAllAsync();

            return View(model);
        }

        // GET: EmployeeController/Details/5
        public async Task<IActionResult> Details(int id, string viewName = "Details")

        {
            if (id == null) return BadRequest("InNalid Id");
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id);

            if (employee == null) return NotFound(new
            {
                StateCode = 404,
                Message = $"Department With {id}, Not Found"
            });

            var model = _mapper.Map<CreateEmployeeDto>(employee);

            return View(viewName, model);
        }


        // GET: EmployeeController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return BadRequest("Invalid Id");
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id);
            if (employee is null) return NotFound(
                new
                {
                    StatusCode = 404,
                    Message = $"Employee With {id} Not Found"
                });

            var model = _mapper.Map<CreateEmployeeDto>(employee);

            return View(model);
            //Refactoring
            //return Details(id, "Delete");
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;

                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = await _unitOfWork.ComplatedAsync();

                if (count > 0)
                {
                    if (employee.ImageName is not null)
                    {
                        DocumentSetting.Delete(employee.ImageName, "Images");
                    }
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }


    }
}
