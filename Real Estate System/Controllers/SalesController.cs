using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_Estate_System.Models;
using Real_Estate_System.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using Microsoft.EntityFrameworkCore;
 


namespace Real_Estate_System.Controllers
{
    [Authorize(Roles = "Admin,Sales,salesMember")]
    public class SalesController : Controller
    {
        private readonly RealStateRepository<Sales> salesRepository;
        private readonly RealStateDbContext _db;

        public SalesController(RealStateRepository<Sales> salesRepository,RealStateDbContext db)
        {
            this.salesRepository = salesRepository;
          _db = db;
        }
        // GET: SalesController
        public ActionResult Index(string searchEngine, string sortOrder, string sortAge, int pageNumber=1,int pageSize=5)
        {            
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.FullNameSortParam = string.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.CurrentsortAge = sortAge;
            ViewBag.AgeSortParam = string.IsNullOrEmpty(sortAge) ? "Age_desc" : "";
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var saller = from b in salesRepository.List()
                         select b;
            //Search and Filtering 
            var sallerCount = saller.Count();
            if (!String.IsNullOrEmpty(searchEngine))
            {
                saller = saller.Where(b => b.FullName.Contains(searchEngine));
                sallerCount = saller.Count();
            }
            //Sorting Logic by Full Name
            switch (sortOrder)
            {
                case "Name_desc":
                    saller = saller.OrderByDescending(b => b.FullName);
                    break;
                default:
                    saller = saller.OrderBy(b => b.FullName);
                    break;
            }
            //Sorting Logic by Age
            switch (sortAge)
            {
                case "Age_desc":
                    saller = saller.OrderByDescending(b => b.Age);
                    break;
                default:
                    saller = saller.OrderBy(b => b.Age);
                    break;
            }
            saller =saller
            .Skip(ExcludeRecords).Take(pageSize);
            var result = new PagedResult<Sales>
            {
                Data = saller.AsQueryable().ToList(),
                TotalItems = sallerCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return View(result);
        }

        // GET: SalesController/Details/5
        public ActionResult Details(int id)
        {
            var saller = salesRepository.Find(id);
            return View(saller);
        }

        // GET: SalesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sales saller)
        {
            try
            {
                salesRepository.Add(saller);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesController/Edit/5
        public ActionResult Edit(int id)
        {
            var saller = salesRepository.Find(id);
            return View(saller);
        }

        // POST: SalesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Sales saller)
        {
            try
            {
                salesRepository.Update(id, saller);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesController/Delete/5
        public ActionResult Delete(int id)
        {
            var saller = salesRepository.Find(id);
            return View(saller);
        }

        // POST: SalesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Sales saller)
        {
            try
            {
                salesRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
