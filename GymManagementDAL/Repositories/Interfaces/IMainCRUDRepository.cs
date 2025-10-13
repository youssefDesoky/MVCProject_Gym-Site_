using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces;

public interface IMainCRUDRepository<TEntity> where TEntity : BaseEntity
{
    TEntity? GetById(int id);
    int Create(TEntity entity);
    int Update(TEntity entity);
    int Delete(TEntity entity);
    IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null);
}
