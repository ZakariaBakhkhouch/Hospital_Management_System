using System.Collections.Generic;

namespace S.G.H.Models.Repositories
{ 
    public interface IPatientRepository<TEntity>
    {
        List<TEntity> GetPatientsList();

        TEntity Find(int id);

        void Delete(int id);

        void Add(TEntity entity);

        void Update(TEntity entity, int id);

        List<TEntity> Search(string prenom);
    }
}
