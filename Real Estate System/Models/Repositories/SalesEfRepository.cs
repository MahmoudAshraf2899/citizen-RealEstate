using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Models.Repositories
{
    public class SalesEfRepository : RealStateRepository<Sales>
    {
        private readonly RealStateDbContext db;

        public SalesEfRepository(RealStateDbContext _db)
        {
            db = _db;
        }
        public void Add(Sales entity)
        {
            db.Sallers.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var seller = Find(id);
            db.Sallers.Remove(seller);
            db.SaveChanges();
        }

        public Sales Find(int id)
        {
            var seller = db.Sallers.SingleOrDefault(s => s.Id == id);
            return seller;
        }

        public IList<Sales> List()
        {
            return db.Sallers.ToList();
        }

        public void Update(int id, Sales newSaller)
        {
            db.Update(newSaller);
            db.SaveChanges();
        }
    }
}
