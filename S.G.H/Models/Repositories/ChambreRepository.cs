using Microsoft.EntityFrameworkCore;
using S.G.H.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace S.G.H.Models.Repositories
{
    public class ChambreRepository : IChambreRepository<Chambre>
    {
        
        AppDbContext dbContext;

        public ChambreRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public List<Chambre> GetChambres()
        {
            return dbContext.Chambres.Include(a => a.Patient).ToList();
        }


        public Chambre Find(int id)
        {
            Chambre chambre = dbContext.Chambres.Include(a => a.Patient).SingleOrDefault(a => a.Id == id);
            return chambre;
        }


        public Chambre Find_2(int nombre)
        {
            Chambre chambre = dbContext.Chambres.Include(a => a.Patient).SingleOrDefault(a => a.Nombre == nombre);
            return chambre;
        }


        public void Update(int id,Chambre newchambre)
        {
            dbContext.Chambres.Update(newchambre);
            dbContext.SaveChanges(true);
        }
        

        //public void Vide(PatientChambreViewModel viewModel, int id)
        //{
        //    dbContext.Update(viewModel);
        //    dbContext.SaveChanges();
        //}
    }
}
