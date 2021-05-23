using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Models.Repositories
{
    public class SalesRepository : RealStateRepository<Sales>
    {
        IList<Sales> Sallers;
        public SalesRepository()
        {
            Sallers = new List<Sales>()
            {
                new Sales
                {
                    Id=1, Address="Gesr Elsues", Age=25, Email="mahmoud@fr.com", FullName="Mahmoud Ashraf", Phone=01148825777, Salary=5000
                },
                 new Sales
                {
                    Id=2, Address="PortSaid", Age=20, Email="menna@fr.com", FullName="Menna Mahmoud", Phone=0100782111, Salary=4000
                },
                  new Sales
                {
                    Id=3, Address="Aswan", Age=24, Email="Rana@fr.com", FullName="Rana Moustafa", Phone=45848484, Salary=3000
                },
            };
        }
        public void Add(Sales entity)
        {
            Sallers.Add(entity);
        }

        public void Delete(int id)
        {
            var seller = Find(id);
            Sallers.Remove(seller);
        }

        public Sales Find(int id)
        {
            var seller = Sallers.SingleOrDefault(s => s.Id == id);
            return seller;  
        }

        public IList<Sales> List()
        {
            return Sallers;
        }

        public void Update(int id, Sales newSaller)
        {
            var Seller = Find(id);
            Seller.Address = newSaller.Address;
            Seller.Age = newSaller.Age;
            Seller.Email = newSaller.Email;
            Seller.FullName = newSaller.FullName;
            Seller.Phone = newSaller.Phone;
            Seller.Salary = newSaller.Salary;
        }
    }
}
