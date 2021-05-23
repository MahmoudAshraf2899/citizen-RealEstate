using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Real_Estate_System.Models;
using Real_Estate_System.Models.Repositories;
using Real_Estate_System.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Controllers
{
    [Authorize(Roles = "Admin,Sales,salesMember,CallCenter,callcenterMember,Excutive")]

    public class DublexController : Controller
    {
        private readonly RealStateRepository<Dublex> dublexRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly RealStateDbContext _db;

        public DublexController(RealStateRepository<Dublex> dublexRepository, IHostingEnvironment hostingEnvironment,RealStateDbContext db)
        {
            this.dublexRepository = dublexRepository;
            this.hostingEnvironment = hostingEnvironment;
           _db = db;
        }
        // GET: DublexController
        public ActionResult Index()
        {
            var dublex = dublexRepository.List();
            return View(dublex);
        }
        // GET: DublexController/Details/5
        public ActionResult Details(int id)
        {
            var dublex = dublexRepository.Find(id);
            return View(dublex);
        }
        // GET: DublexController/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: DublexController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DublexCreateViewModel model, IFormFile[] Files)
        {
            try
            {
                string fileName = string.Empty;
                string allFilesName = string.Empty;
                if (Files != null)
                {
                    foreach (var file in Files)
                    {

                        string uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                        fileName = file.FileName;
                        string fullPath = Path.Combine(uploads, fileName);
                        allFilesName += fileName + "$$##";
                        file.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                }
                Dublex newDublex = new Dublex
                {
                    Address = model.Address,
                    Area = model.Area,
                    Informations = model.Informations,
                    Payment = model.Payment,
                    Phone = model.Phone,
                    Price = model.Price,
                    ImageUrl = allFilesName
                };
                dublexRepository.Add(newDublex);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: DublexController/Edit/5
        public ActionResult Edit(int id)
        {
            var dublex = dublexRepository.Find(id);
            return View(dublex);
        }
        // POST: DublexController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dublex dublex, IFormFile[] Files)
        {
            try
            {
                string fileName = string.Empty;
                string allFilesName = string.Empty;
                if (Files != null)
                {
                    foreach (var file in Files)
                    {
                        string uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                        fileName = file.FileName;
                        string fullPath = Path.Combine(uploads, fileName);
                        allFilesName += fileName + "$$##";
                        file.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                }
                Dublex newDublex = new Dublex
                {
                    Address = dublex.Address,
                    Area = dublex.Area,
                    Informations = dublex.Informations,
                    Payment = dublex.Payment,
                    Phone = dublex.Phone,
                    Price = dublex.Price,
                    ImageUrl = allFilesName
                };
                dublexRepository.Update(id, dublex);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: DublexController/Delete/5
        public ActionResult Delete(int id)
        {
            var dublex = dublexRepository.Find(id);
            return View(dublex);
        }
        // POST: DublexController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dublex dublex)
        {
            try
            {
                dublexRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
