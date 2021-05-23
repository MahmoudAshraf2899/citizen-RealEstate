using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System.Models.Repositories
{
    public class DublexRepository : RealStateRepository<Dublex>
    {
        List<Dublex> dublexes;
        public DublexRepository()
        {
            dublexes = new List<Dublex>
            {
                new Dublex
                {
                    Id=1, Area=150, Payment="Cash", Address="New Cairo", Informations="Appartment For Sale" ,Price=750000  ,Phone=0100782111, ImageUrl="download.jpg"

                },
                 new Dublex
                {
                    Id=2, Area=80, Payment="Cash", Address="6 October", Informations="Appartment For Rent" ,Price=960000  ,Phone=0100782111, ImageUrl="download.jpg"

                },
                  new Dublex
                {
                    Id=3, Area=100, Payment="Cash", Address="Elshrouq", Informations="Appartment For Sale" ,Price=1500000 ,Phone=0100782111, ImageUrl="download.jpg"

                },
            };
        }
        public void Add(Dublex entity)
        {
            dublexes.Add(entity);
        }

        public void Delete(int id)
        {
            var dublex = Find(id);
            dublexes.Remove(dublex);
        }

        public Dublex Find(int id)
        {
            var dublex = dublexes.SingleOrDefault(d => d.Id == id);
            return dublex;
        }

        public IList<Dublex> List()
        {
          return dublexes;
        }

        public void Update(int id, Dublex newDublex)
        {
            var dublex = Find(id);
            dublex.Area = newDublex.Area;
            dublex.Payment = newDublex.Payment;
            dublex.Address = newDublex.Address;
            dublex.Informations = newDublex.Informations;
            dublex.Price = newDublex.Price;
            dublex.Phone = newDublex.Phone;




        }
    }
}
