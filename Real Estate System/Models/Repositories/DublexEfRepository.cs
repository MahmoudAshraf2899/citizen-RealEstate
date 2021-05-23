using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Models.Repositories
{
    public class DublexEfRepository : RealStateRepository<Dublex>
    {
        private readonly RealStateDbContext db;

        public DublexEfRepository(RealStateDbContext _db)
        {
            db = _db;
        }
        public void Add(Dublex entity)
        {
            db.Dublexes.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var dublex = Find(id);
            db.Remove(dublex);
            db.SaveChanges();
        }

        public Dublex Find(int id)
        {
            var dublex = db.Dublexes.SingleOrDefault(d => d.Id == id);
            return dublex;
        }

        public IList<Dublex> List()
        {
            return db.Dublexes.ToList();
        }

        public void Update(int id, Dublex newDublex)
        {
            db.Update(newDublex);
            db.SaveChanges();             
        }
    }
}
