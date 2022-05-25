using Microsoft.EntityFrameworkCore;
using S.G.H.Models.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace S.G.H.Models.Repositories
{
    public class FactureRepository : IFactureRepository<Facture>
    {
        AppDbContext dbContext;

        public FactureRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



        public List<Facture> GetFactures()
        {
            return dbContext.Factures.Include(a => a.Patient).ToList();
        }



        public Facture Find(int id)
        {
            var facture = dbContext.Factures.Include(b => b.Patient).SingleOrDefault(a => a.Id == id);
            return facture;
        }



        public void Add(Facture entity)
        {
            dbContext.Add(entity);
            dbContext.SaveChanges();
        }



        public List<Facture> Search(string number)
        {
            List<Facture> result;

            if(number == null)
            {
                return GetFactures();
            }
            else
            {
                result = dbContext.Factures.Include(a => a.Patient).Where(b => b.Nombre.ToString() == number).ToList();
            }
            return result;
        }
    }
}
