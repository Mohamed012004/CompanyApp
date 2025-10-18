using Company.Route.DAL.Models;
using Company.Route.PL.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Route.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManager<AppUser> _userMaanager { get; }

        public RoleController(RoleManager<IdentityRole> roleManager,
                               UserManager<AppUser> userMaanager)
        {
            _roleManager = roleManager;
            _userMaanager = userMaanager;
        }


        // GET: EmployeeController
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> roles;
            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name
                });
            }
            else
            {

                roles = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name
                }).Where(U => U.Name.ToLower().Contains(SearchInput.ToLower()));
            }


            return View(roles);

        }



        // GET: EmployeeController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: EmployeeController/Create
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByNameAsync(model.Name);
                if (role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.Name
                    };
                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }

                ModelState.AddModelError("", "Invalid operation !!");
            }
            return View(model);

        }



        // GET: EmployeeController/Details/5
        public async Task<IActionResult> Details(string? id, string viewName = "Details")

        {
            if (id == null) return BadRequest("Invalid Id");
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null) return NotFound(new
            {
                StateCode = 404,
                Message = $"Role With {id}, Not Found"
            });

            var roleDto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(viewName, roleDto);
        }



        // GET: EmployeeController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            #region Manaul
            //if (id == null) return BadRequest("InValid Id");
            //var user = await _userManager.FindByIdAsync(id);

            //if (user == null) return NotFound(new
            //{
            //    StateCode = 404,
            //    Message = $"Department With {id}, Not Found"
            //});

            //return View(user); 
            #endregion

            //Refactoring
            return await Details(id, "Edit");
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Id");
                var role = await _roleManager.FindByIdAsync(id);

                if (role is null) return NotFound(
                    new
                    {
                        StatusCode = 404,
                        Message = $"User With {id} Not Found"
                    }
                    );

                var roleResult = await _roleManager.FindByIdAsync(model.Name);

                if (roleResult is null)
                {
                    role.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError("", "Invalid operation !!");
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
        public async Task<ActionResult> Delete([FromRoute] string id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Id");
                var role = await _roleManager.FindByIdAsync(id);

                if (role is null) return NotFound(
                    new
                    {
                        StatusCode = 404,
                        Message = $"User With {id} Not Found"
                    }
                    );


                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Invalid operation !!");
            }

            return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return NotFound();
            ViewData["RoleId"] = roleId;

            ViewData["RoleId"] = roleId;

            var UsersInRole = new List<UserInRoleDto>();
            var Users = await _userMaanager.Users.ToListAsync();

            foreach (var user in Users)
            {
                var userInRole = new UserInRoleDto()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userMaanager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }

                UsersInRole.Add(userInRole);

            }

            return View(UsersInRole);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UserInRoleDto> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return NotFound();


            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userMaanager.FindByIdAsync(user.UserId);
                    if (appUser is not null)
                    {
                        if (user.IsSelected && !await _userMaanager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userMaanager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if (!user.IsSelected && await _userMaanager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userMaanager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }
                }
                return RedirectToAction(nameof(Edit), new { id = roleId });

            }

            return View(users);
        }



    }
}
