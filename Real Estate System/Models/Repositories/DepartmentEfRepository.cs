using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Models.Repositories
{
    public class DepartmentEfRepository : RealStateRepository<Department>
    {
        private readonly RealStateDbContext db;

        public DepartmentEfRepository(RealStateDbContext _db)
        {
            db = _db;
        }
        public void Add(Department entity)
        {
            db.Departments.Add(entity);
            db.SaveChanges();

        }

        public void Delete(int id)
        {
            var department = Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();

        }

        public Department Find(int id)
        {
            var department = db.Departments.SingleOrDefault(d => d.Id == id);
            return department;

        }

        public IList<Department> List()
        {
            return db.Departments.ToList();

        }

        public void Update(int id, Department newDepartment)
        {
            db.Update(newDepartment);
            db.SaveChanges();
        }
    }
}

 