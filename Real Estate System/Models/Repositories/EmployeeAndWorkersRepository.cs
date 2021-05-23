using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Models.Repositories
{
    public class EmployeeAndWorkersRepository : RealStateRepository<Employess_and_workers>
    {
        List<Employess_and_workers> Workers;
        public EmployeeAndWorkersRepository()
        {
            Workers = new List<Employess_and_workers>
            {
                new Employess_and_workers
                {
                    Id=1, Age=25, Email="Worker@fr.com", FullName="Ali", Phone=01447 ,Salary=1500

                },
                 new Employess_and_workers
                {
                    Id=2, Age=25, Email="Worker@fr.com", FullName="Ahmed ", Phone=01447 ,Salary=1500

                },
                  new Employess_and_workers
                {
                    Id=3, Age=25, Email="Worker@fr.com", FullName="Abdo", Phone=01447 ,Salary=1500

                },
            };
        }

        public void Add(Employess_and_workers entity)
        {
            Workers.Add(entity);
        }

        public void Delete(int id)
        {
            var worker = Find(id);
            Workers.Remove(worker);
        }

        public Employess_and_workers Find(int id)
        {
            var worker = Workers.SingleOrDefault(d => d.Id == id);
            return worker;
        }

        public IList<Employess_and_workers> List()
        {
            return Workers;

        }

        public void Update(int id, Employess_and_workers newWorker)
        {
            var worker = Find(id);
            worker.Age = newWorker.Age;
            worker.Email = newWorker.Email;
            worker.FullName = newWorker.FullName;
            worker.Salary = newWorker.Salary;
            worker.Phone = newWorker.Phone;
        }
    }
}
