using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_Estate_System.Models;
using Real_Estate_System.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Controllers
{
    [Authorize(Roles = "Admin,Workers")]

    public class EmployessandworkersController : Controller
    {
        private readonly RealStateRepository<Employess_and_workers> employeesRepository;
        private readonly RealStateDbContext _db;

        public EmployessandworkersController(RealStateRepository<Employess_and_workers> employeesRepository,RealStateDbContext db)
        {
            this.employeesRepository = employeesRepository;
            _db = db;
        }
        // GET: EmployessandworkersController
        public ActionResult Index(string searchEngine, string sortOrder, int pageNumber = 1, int pageSize = 5)
        {
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.FullNameSortParam = string.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var employee = from b in employeesRepository.List()
                           select b;
            //Search and Filtering 
            var employeeCount = employee.Count();
            if (!String.IsNullOrEmpty(searchEngine))
            {
                employee = employee.Where(b => b.FullName.Contains(searchEngine));
                employeeCount = employee.Count();
            }
            //Sorting Logic
            switch (sortOrder)
            {
                case "Name_desc":
                    employee = employee.OrderByDescending(b => b.FullName);
                    break;
                default:
                    employee = employee.OrderBy(b => b.FullName);
                    break;
            }
            employee = employee.Skip(ExcludeRecords).Take(pageSize);
            var result = new PagedResult<Employess_and_workers>
            {
                Data = employee.AsQueryable().ToList(),
                TotalItems = employeeCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return View(result);
        }

        // GET: EmployessandworkersController/Details/5
        public ActionResult Details(int id)
        {
            var employee = employeesRepository.Find(id);
            return View(employee);
        }

        // GET: EmployessandworkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployessandworkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employess_and_workers employess_And_Workers)
        {
            try
            {
                employeesRepository.Add(employess_And_Workers);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployessandworkersController/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = employeesRepository.Find(id);
            return View(employee);
        }

        // POST: EmployessandworkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employess_and_workers employess_And_Workers)
        {
            try
            {
                employeesRepository.Update(id, employess_And_Workers);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployessandworkersController/Delete/5
        public ActionResult Delete(int id)
        {
            var employee = employeesRepository.Find(id);
            return View(employee);
        }

        // POST: EmployessandworkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Employess_and_workers employess_And_Workers)
        {
            try
            {
                employeesRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
