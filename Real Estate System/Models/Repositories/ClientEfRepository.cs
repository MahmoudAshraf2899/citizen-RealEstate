using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Models.Repositories
{
    public class ClientEfRepository: RealStateRepository<Client>
    {
        private readonly RealStateDbContext db;

    public ClientEfRepository(RealStateDbContext _db)
    {
            db = _db;
    }

    public void Add(Client entity)
    {
        db.Clients.Add(entity);
        db.SaveChanges();
    }

        public void Delete(int id)
    {
        var buyer = Find(id);
        db.Clients.Remove(buyer);
        db.SaveChanges();
    }

    public Client Find(int id)
    {
        var buyer = db.Clients.SingleOrDefault(b => b.Id == id);
        return buyer;
    }

    public IList<Client> List()
    {
            return db.Clients.ToList();
    }

    public void Update(int id, Client newClient)
    {
            db.Update(newClient);
            db.SaveChanges();
        }
    
    }
}


