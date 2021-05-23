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
    [Authorize(Roles = "Admin,Sales,salesMember,CallCenter,callcenterMember")]

    public class ClientController : Controller
    {
        private readonly RealStateRepository<Client> clientRepository;
        private readonly RealStateDbContext _db;

        public ClientController(RealStateRepository<Client> clientRepository,RealStateDbContext db)
        {
            this.clientRepository = clientRepository;
            _db = db;
        }
        // GET: ClientController
        public ActionResult Index(string searchEngine, string sortOrder, string sortBudget, int pageNumber = 1, int pageSize = 5)
        {           
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.FullNameSortParam = string.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.CurrentsortBudget = sortBudget;
            ViewBag.BudgetSortParam = string.IsNullOrEmpty(sortBudget) ? "Budget_desc" : "";
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var client = from b in clientRepository.List()
                         select b;
            //Search and Filtering 
            var clientCount = client.Count();
            if (!String.IsNullOrEmpty(searchEngine))
            {
                client = client.Where(b => b.FullName.Contains(searchEngine));
                clientCount = client.Count();
            }
            //Sorting Logic by Name
            switch (sortOrder)
            {
                case "Name_desc":
                    client = client.OrderByDescending(b => b.FullName);
                    break;
                default:
                    client = client.OrderBy(b => b.FullName);
                    break;
            }
            //Sorting Logic by Budget
            switch (sortBudget)
            {
                case "Budget_desc":
                    client = client.OrderByDescending(b => b.Budget);
                    break;
                default:
                    client = client.OrderBy(b => b.Budget);
                    break;
            }
            client = client.Skip(ExcludeRecords).Take(pageSize);
            var result = new PagedResult<Client>
            {
                Data = client.AsQueryable().ToList(),
                TotalItems = clientCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return View(result);
        }

        // GET: ClientController/Details/5
        public ActionResult Details(int id)
        {
            var client = clientRepository.Find(id);
            return View(client);
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client client)
        {
            try
            {
                clientRepository.Add(client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = clientRepository.Find(id);
            return View(client);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Client client)
        {
            try
            {
                clientRepository.Update(id, client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = clientRepository.Find(id);
            return View(client);
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Client client)
        {
            try
            {
                clientRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
