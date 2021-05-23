using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Models.Repositories
{
    public class EmployeesandworkersEfRepository : RealStateRepository<Employess_and_workers>
    {
        private readonly RealStateDbContext db;

        public EmployeesandworkersEfRepository(RealStateDbContext _db)
        {
            db = _db;
        }

        public void Add(Employess_and_workers entity)
        {
            db.employess_And_Workers.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var worker = Find(id);
            db.employess_And_Workers.Remove(worker);
            db.SaveChanges();
        }

        public Employess_and_workers Find(int id)
        {
            var worker = db.employess_And_Workers.SingleOrDefault(d => d.Id == id);
            return worker;
        }

        public IList<Employess_and_workers> List()
        {
            return db.employess_And_Workers.ToList();

        }

        public void Update(int id, Employess_and_workers newWorker)
        {
            db.Update(newWorker);
            db.SaveChanges();
        }
    }    
}

