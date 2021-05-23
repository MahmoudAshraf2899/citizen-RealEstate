using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Models.Repositories
{
    public class CallCenterEfRepository : RealStateRepository<CallCenter>
    {
        private readonly RealStateDbContext db;

        public CallCenterEfRepository(RealStateDbContext _db)
        {
            db = _db;
        }
        public void Add(CallCenter entity)
        {
            db.CallCenters.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var center = Find(id);
            db.CallCenters.Remove(center);
            db.SaveChanges();
        }

        public CallCenter Find(int id)
        {
            var center = db.CallCenters.SingleOrDefault(d => d.Id == id);
            return center;
        }

        public IList<CallCenter> List()
        {
            return db.CallCenters.ToList();
        }

        public void Update(int id, CallCenter newcallCenter)
        {
            db.Update(newcallCenter);
            db.SaveChanges();
        }
    }
}

 
