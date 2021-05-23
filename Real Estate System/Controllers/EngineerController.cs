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
    [Authorize(Roles = "Admin,Engineer,engineerMember")]

    public class EngineerController : Controller
    {
        private readonly RealStateRepository<Engineer> engineerRepository;
        private readonly RealStateDbContext _db;

        public EngineerController(RealStateRepository<Engineer> engineerRepository,RealStateDbContext db)
        {
            this.engineerRepository = engineerRepository;
            _db = db;
        }
        // GET: EngineerController
        public ActionResult Index(string searchEngine, string sortOrder, int pageNumber = 1, int pageSize = 5)
        {
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.FullNameSortParam = string.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var eng = from b in engineerRepository.List()
                      select b;
            //Search and Filtering 
            var engCount = eng.Count();
            if (!String.IsNullOrEmpty(searchEngine))
            {
                eng = eng.Where(b => b.FullName.Contains(searchEngine));
                engCount = eng.Count();
            }
            //Sorting Logic
            switch (sortOrder)
            {
                case "Name_desc":
                    eng = eng.OrderByDescending(b => b.FullName);
                    break;
                default:
                    eng = eng.OrderBy(b => b.FullName);
                    break;
            }
            eng = eng
            .Skip(ExcludeRecords).Take(pageSize);
            var result = new PagedResult<Engineer>
            {
                Data = eng.AsQueryable().ToList(),
                TotalItems = engCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return View(result);
        }

        // GET: EngineerController/Details/5
        public ActionResult Details(int id)
        {
            var eng = engineerRepository.Find(id);
            return View(eng);
        }

        // GET: EngineerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EngineerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Engineer engineer)
        {
            try
            {
                engineerRepository.Add(engineer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EngineerController/Edit/5
        public ActionResult Edit(int id)
        {
            var eng = engineerRepository.Find(id);
            return View(eng);
        }

        // POST: EngineerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Engineer engineer)
        {
            try
            {
                engineerRepository.Update(id, engineer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EngineerController/Delete/5
        public ActionResult Delete(int id)
        {
            var eng = engineerRepository.Find(id);
            return View(eng);
        }

        // POST: EngineerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Engineer engineer)
        {
            try
            {
                engineerRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
