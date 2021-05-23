using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Real_Estate_System.Models;
using Real_Estate_System.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Controllers
{
    public class UsersController : Controller
    {
        private readonly RealStateDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(RealStateDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string searchEngine, string sortOrder, int pageNumber = 1, int pageSize = 5)
        {
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.FullNameSortParam = string.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var user = from b in _db.Users.ToList()
                       select b;
            //Search and Filtering 
            var userCount = user.Count();
            if (!String.IsNullOrEmpty(searchEngine))
            {
                user = user.Where(b => b.UserName.Contains(searchEngine));
                userCount = user.Count();
            }
            //Sorting Logic
            switch (sortOrder)
            {
                case "Name_desc":
                    user = user.OrderByDescending(b => b.UserName);
                    break;
                default:
                    user = user.OrderBy(b => b.UserName);
                    break;
            }
            user =user.Skip(ExcludeRecords).Take(pageSize);
            var result = new PagedResult<IdentityUser>
            {
                Data = user.AsQueryable().ToList(),
                TotalItems = userCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return View(result);
        }

        // GET: UsersController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        // POST: AdminController/Edit/5
        [HttpPost, ActionName("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel userView)
        {
            var user = await _userManager.FindByIdAsync(userView.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"UserID :{userView.Id} of this customer is not found.";
                return View("NotFound");
            }
            else
            {
                user.UserName = userView.UserName;
                user.Email = userView.Email;
                user.PhoneNumber = userView.PhoneNumber;
               

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(userView);
        }

        // GET: AdminController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        // POST: AdminController/Delete/5
        [HttpPost, ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("Index");
            }
        }
    }
}
