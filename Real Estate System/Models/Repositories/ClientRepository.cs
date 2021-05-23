using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Models.Repositories
{
    public class ClientRepository : RealStateRepository<Client>
    {
        List<Client> clients;
        public ClientRepository()
        {
            clients = new List<Client>
            {
                new Client
                {
                    Id=1, Phone=0100875442, PhoneNumber=024651258, FullName="Mahmoud" ,Budget=650000, Email="Client@fr.com", Requierments="يبحث عن شقة في الدور الاول في منطقة العبور تكون علي مساحة 140 متر"

                },
                 new Client
                {
                   Id=1, Phone=0100875442, PhoneNumber=024651258, FullName="Mahmoud" , Budget=650000, Email="Client@fr.com", Requierments="يبحث عن شقة في الدور الاول في منطقة العبور تكون علي مساحة 140 متر"

                },
                  new Client
                {
                   Id=1, Phone=0100875442, PhoneNumber=024651258, FullName="Mahmoud" , Budget=450000,  Email="Client@fr.com", Requierments="يبحث عن شقة في الدور الاول في منطقة العبور تكون علي مساحة 140 متر"

                },
            };
        }

        public void Add(Client entity)
        {
            clients.Add(entity);
        }

        public void Delete(int id)
        {
            var buyer = Find(id);
            clients.Remove(buyer);
        }

        public Client Find(int id)
        {
            var buyer = clients.SingleOrDefault(b => b.Id == id);
            return buyer;
        }

        public IList<Client> List()
        {
            return clients;
        }

        public void Update(int id, Client newClient)
        {
            var buyer = Find(id);
            buyer.Phone = newClient.Phone;
            buyer.PhoneNumber = newClient.PhoneNumber;
            buyer.Email = newClient.Email;
            buyer.Budget = newClient.Budget;
            buyer.Requierments = newClient.Requierments;



        }
    }
}
