using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Models.Repositories
{
    public class EngineerEfRepository : RealStateRepository<Engineer>
    {
        private readonly RealStateDbContext db;

        public EngineerEfRepository(RealStateDbContext _db)
        {
            db = _db;
        }

        public void Add(Engineer entity)
        {
            db.Engineers.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var eng = Find(id);
            db.Engineers.Remove(eng);
            db.SaveChanges();
        }

        public Engineer Find(int id)
        {
            var eng = db.Engineers.SingleOrDefault(e => e.Id == id);
            return eng;
        }

        public IList<Engineer> List()
        {
            return db.Engineers.ToList();

        }

        public void Update(int id, Engineer newEngineer)
        {
            db.Update(newEngineer);
            db.SaveChanges();
        }
    }    

}

