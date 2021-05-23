using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Models.Repositories
{
    public class DepartmentRepository : RealStateRepository<Department>
    {
        List<Department> departments;

        public DepartmentRepository()
        {
            departments = new List<Department>
            {
                new Department
                {
                    Id=1, departmentName="Sales"

                },
                 new Department
                {
                     Id=2, departmentName="Engineer"

                },
                  new Department
                {
                    Id=1, departmentName="CallCenter"

                },
            };
        }
        public void Add(Department entity)
        {
            departments.Add(entity);

        }

        public void Delete(int id)
        {
            var department = Find(id);
            departments.Remove(department);
        }

        public Department Find(int id)
        {
            var department = departments.SingleOrDefault(d => d.Id == id);
            return department;

        }

        public IList<Department> List()
        {
            return departments;

        }

        public void Update(int id, Department newDepartment)
        {
            var department = Find(id);
            department.departmentName = newDepartment.departmentName;
        }
    }
}
