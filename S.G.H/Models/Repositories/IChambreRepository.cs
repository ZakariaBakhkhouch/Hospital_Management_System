using System.Collections.Generic;

namespace S.G.H.Models.Repositories
{
    public interface IChambreRepository<TEntity>
    {
        List<TEntity> GetChambres();

        TEntity Find(int id);

        void Update(int id,TEntity entity);

        TEntity Find_2(int id);
    }
}
