using Company.Route.DAL.Models;
using Company.Route.PL.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Route.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        // GET: EmployeeController
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<UserToReturnDto> users;
            if (string.IsNullOrEmpty(SearchInput))
            {
                users = _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id = U.Id,
                    UserName = U.UserName,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Email = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result
                });
            }
            else
            {

                users = _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id = U.Id,
                    UserName = U.UserName,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Email = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result
                }).Where(U => U.FirstName.ToLower().Contains(SearchInput.ToLower()));
            }


            return View(users);

        }


        // GET: EmployeeController/Details/5
        public async Task<IActionResult> Details(string id, string viewName = "Details")

        {
            if (id == null) return BadRequest("InNalid Id");
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return NotFound(new
            {
                StateCode = 404,
                Message = $"User With {id}, Not Found"
            });

            var userDto = new UserToReturnDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(viewName, userDto);
        }



        // GET: EmployeeController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            //if (id == null) return BadRequest("InValid Id");
            //var user = await _userManager.FindByIdAsync(id);

            //if (user == null) return NotFound(new
            //{
            //    StateCode = 404,
            //    Message = $"Department With {id}, Not Found"
            //});

            //return View(user);

            //Refactoring
            return await Details(id, "Edit");
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Id");
                var user = await _userManager.FindByIdAsync(id);

                if (user is null) return NotFound(
                    new
                    {
                        StatusCode = 404,
                        Message = $"User With {id} Not Found"
                    }
                    );

                // Update Data
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }


            return View(model);
        }


        // GET: EmployeeController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            #region Temp

            //if (id == null) return BadRequest("Invalid Id");
            //var user = await _userManager.FindByIdAsync(id);
            //if (user is null) return NotFound(
            //    new
            //    {
            //        StatusCode = 404,
            //        Message = $"User With {id} Not Found"
            //    });

            //var userDto = new UserToReturnDto()
            //{
            //    Id = user.Id,
            //    UserName = user.UserName,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Email = user.Email,
            //    Roles = _userManager.GetRolesAsync(user).Result
            //};
            //return View(userDto); 
            #endregion

            //Refactoring
            return await Details(id, "Delete");
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([FromRoute] string id, UserToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Id");
                var user = await _userManager.FindByIdAsync(id);

                if (user is null) return NotFound(
                    new
                    {
                        StatusCode = 404,
                        Message = $"User With {id} Not Found"
                    }
                    );


                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }



    }
}
