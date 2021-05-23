using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Models.Repositories
{
    public class CallCenterRepository : RealStateRepository<CallCenter>
    {
        List<CallCenter> Centers;
        public CallCenterRepository()
        {
            Centers = new List<CallCenter>
            {
                new CallCenter
                {
                    Id=1, Age=25, Email="CallCenter@fr.com", FullName="Omar Fahmy", Phone=01447 ,Salary=1500

                },
                 new CallCenter
                {
                    Id=2, Age=25, Email="CallCenter@fr.com", FullName="Omar ", Phone=01447 ,Salary=1500

                },
                  new CallCenter
                {
                    Id=3, Age=25, Email="CallCenter@fr.com", FullName="Fahmy", Phone=01447 ,Salary=1500

                },
            };
        }
        public void Add(CallCenter entity)
        {
            Centers.Add(entity);

        }

        public void Delete(int id)
        {
            var center = Find(id);
            Centers.Remove(center);
        }

        public CallCenter Find(int id)
        {
            var center = Centers.SingleOrDefault(d => d.Id == id);
            return center;

        }

        public IList<CallCenter> List()
        {
            return Centers;

        }

        public void Update(int id, CallCenter newcallCenter)
        {
            var center = Find(id);
            center.Age = newcallCenter.Age;
            center.Email = newcallCenter.Email;
            center.FullName = newcallCenter.FullName;
            center.Salary = newcallCenter.Salary;
            center.Phone = newcallCenter.Phone;
        }
    }
}
