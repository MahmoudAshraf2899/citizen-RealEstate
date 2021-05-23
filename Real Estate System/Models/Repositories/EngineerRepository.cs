using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Models.Repositories
{
    public class EngineerRepository : RealStateRepository<Engineer>
    {
        List<Engineer> Engineers;
        public EngineerRepository()
        {
            Engineers = new List<Engineer>
            {
                new Engineer
                {
                    Id=1, Age=25, Email="Engineer@fr.com", FullName="Ahmed Gizawy", Phone=014565647 ,Salary=1500

                },
                 new Engineer
                {
                    Id=2, Age=35, Email="Engineer@fr.com", FullName="Gizawy ", Phone=0144564847 ,Salary=1500

                },
                  new Engineer
                {
                    Id=3, Age=45, Email="Engineer@fr.com", FullName="Khairy", Phone=868889 ,Salary=1500

                },
            };
        }

        public void Add(Engineer entity)
        {
            Engineers.Add(entity);

        }

        public void Delete(int id)
        {
            var eng = Find(id);
            Engineers.Remove(eng);
        }

        public Engineer Find(int id)
        {
            var eng = Engineers.SingleOrDefault(e => e.Id == id);
            return eng;
        }

        public IList<Engineer> List()
        {
            return Engineers;

        }

        public void Update(int id, Engineer newEngineer)
        {
            var eng = Find(id);
            eng.Age = newEngineer.Age;
            eng.Email = newEngineer.Email;
            eng.FullName = newEngineer.FullName;
            eng.Salary = newEngineer.Salary;
            eng.Phone = newEngineer.Phone;
        }
    }
}
