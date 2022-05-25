using System.Collections.Generic;
using System.Linq;

namespace S.G.H.Models.Repositories
{
    public class DocteurRepository : IDocteurRepository<Docteur>
    {

        AppDbContext dbContext;

        public DocteurRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public void Add(Docteur docteur)
        {
            dbContext.Docteurs.Add(docteur);
            dbContext.SaveChanges();
        }


        public void Delete(int id)
        {
            var docteur = Find(id);
            dbContext.Docteurs.Remove(docteur);
            dbContext.SaveChanges();
        }


        public Docteur Find(int id)
        {
            Docteur docteur = dbContext.Docteurs.SingleOrDefault(a => a.Matricule == id);
            return docteur;
        }


        public IList<Docteur> GetDocteursList()
        {
            return dbContext.Docteurs.ToList();
        }


        public List<Docteur> Search(string fullname)
        {
            List<Docteur> docteur = new List<Docteur>();

            if(fullname == null)
            {
                docteur = dbContext.Docteurs.ToList();
            }
            else
            {
                docteur = dbContext.Docteurs.Where(a => a.FullName.Contains(fullname)).ToList();
            }
            return docteur;
        }   
         

        public void Update(Docteur newdocteur, int id)
        {
            dbContext.Update(newdocteur);
            dbContext.SaveChanges();
        }
    }
}
