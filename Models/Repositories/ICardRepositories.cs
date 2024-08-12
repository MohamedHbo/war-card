using System.Collections.Generic;

namespace war.Models.Repositories
{
    public interface ICardRepositories<TEntity>
    {
        IList<TEntity> List(); // Use Pascal case for the method name
        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(int id, TEntity entity);
        void Delete(int id);
        void Delete(Card card);
        void Update(Card cardToUpdate);
    }
}
