using System.Collections.Generic;

namespace S.G.H.Models.Repositories
{
    public interface IFactureRepository<TEntity>
    {
        List<TEntity> GetFactures();

        TEntity Find(int id);

        void Add(TEntity entity);    
        
        List<TEntity> Search(string number);
    }
}
