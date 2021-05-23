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
    [Authorize(Roles = "Admin,CallCenter,callcenterMember")]

    public class CallCenterController : Controller
    {
        private readonly RealStateRepository<CallCenter> centerRepository;
        private readonly RealStateDbContext _db;

        public CallCenterController(RealStateRepository<CallCenter> centerRepository,RealStateDbContext db)
        {
            this.centerRepository = centerRepository;
            _db = db;
        }
        // GET: CallCenterController
        public ActionResult Index(string searchEngine, string sortOrder, string sortAge, int pageNumber = 1, int pageSize = 5)
        {
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.FullNameSortParam = string.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.CurrentsortAge = sortAge;
            ViewBag.AgeSortParam = string.IsNullOrEmpty(sortAge) ? "Age_desc" : "";
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var center = from b in centerRepository.List()
                         select b;
            //Search and Filtering 
            var centerCount = center.Count();
            if (!String.IsNullOrEmpty(searchEngine))
            {
                center = center.Where(b => b.FullName.Contains(searchEngine));
                centerCount = center.Count();
            }
            //Sorting Logic by FullName
            switch (sortOrder)
            {
                case "Name_desc":
                    center = center.OrderByDescending(b => b.FullName);
                    break;
                default:
                    center = center.OrderBy(b => b.FullName);
                    break;
            }
            //Sorting Logic by Age
            switch (sortAge)
            {
                case "Age_desc":
                    center = center.OrderByDescending(b => b.Age);
                    break;
                default:
                    center = center.OrderBy(b => b.Age);
                    break;
            }
            center = center.Skip(ExcludeRecords).Take(pageSize);
            var result = new PagedResult<CallCenter>
            {
                Data = center.AsQueryable().ToList(),
                TotalItems = centerCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return View(result);
        }

        // GET: CallCenterController/Details/5
        public ActionResult Details(int id)
        {
            var center = centerRepository.Find(id);
            return View(center);
        }

        // GET: CallCenterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CallCenterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CallCenter callCenter)
        {
            try
            {
                centerRepository.Add(callCenter);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CallCenterController/Edit/5
        public ActionResult Edit(int id)
        {
            var center = centerRepository.Find(id);
            return View(center);
        }

        // POST: CallCenterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CallCenter callCenter)
        {
            try
            {
                centerRepository.Update(id,callCenter);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CallCenterController/Delete/5
        public ActionResult Delete(int id)
        {
            var center = centerRepository.Find(id);
            return View(center);
        }

        // POST: CallCenterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CallCenter callCenter)
        {
            try
            {
                centerRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
