using Company.Route.BLL.Interfaces;
using Company.Route.DAL.Models;
using Company.Route.PL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Company.Route.PL.Controllers
{
    // MVC Controller 
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //public readonly IDepartmentRepository _departmentRepository; // Null
        // ASK  From CLR To Create Objet From DepartmentRepository 
        public DepartmentController(
           /* IDepartmentRepository departmentrepositoy*/ IUnitOfWork unitOfWork) // Recommended To make ctor Against Reference Not Concrate Class 
        {
            //_departmentRepository = departmentrepositoy;
            _unitOfWork = unitOfWork;
        }

        // /Department/Index
        public IActionResult Index()   // Note: Its Perfect To implement Function Against Interface(IActionResult) Not Concrate Class(ActionResult)
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
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

                _unitOfWork.DepartmentRepository.ADD(department);
                var count = _unitOfWork.Compaated();


                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }


        public IActionResult Details(int id, string viewName = "Details")
        {
            if (id == null) return BadRequest("InValid Id");
            var department = _unitOfWork.DepartmentRepository.Get(id);
            if (department is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = $"Department With {id}, Not Found"
                });
            }

            return View(viewName, department);
        }

        #region Update

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null) return BadRequest("Invalid Id");
            var department = _unitOfWork.DepartmentRepository.Get(id);
            if (department is null) return NotFound(new
            {
                StatusCode = 404,
                Message = $"The Department With {id}, Not Found"
            });

            var departmentDto = new CreateDepartmentDto()
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt
            };
            return View(departmentDto);

            //Refactoring
            //return Details(id, viewName: "Edit");
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([FromRoute] int id, Department department)  // Route => Segment
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (id != department.Id) return BadRequest(); // 400

        //        var count = _departmentRepository.Update(department);
        //        if (count > 0)
        //        {
        //            return RedirectToAction("Index");
        //        }

        //    }
        //    return View(department);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department
                {
                    Id = id,
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt,
                };
                _unitOfWork.DepartmentRepository.Update(department);
                var count = _unitOfWork.Compaated();

                if (count > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }


        #endregion

        [HttpGet]
        public IActionResult Delete(int id)
        {
            //if (id == null) return BadRequest("Invsalid Id");

            //var department = _departmentRepository.Get(id);
            //if (department == null) return NotFound(new
            //{
            //    StatusCode = 404,
            //    Message = $"Department With {id}, Not Found"
            //});
            //return View(department);

            // Refactoring
            return Details(id, "Delete");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // Prevent Action To be Accessed By Exit Tools / Software like[Postman - ...]
        public ActionResult Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest(); // 400

                _unitOfWork.DepartmentRepository.Delete(department);
                var count = _unitOfWork.Compaated();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);

        }



    }
}












