using System.Collections.Generic;

namespace S.G.H.Models.Repositories
{
    public interface IDocteurRepository<TEntity>
    {
        IList<TEntity> GetDocteursList();

        TEntity Find(int id);

        void Delete(int id);

        void Add(TEntity entity);

        void Update(TEntity entity,int id);

        List<TEntity> Search(string fullname);
    }
}
