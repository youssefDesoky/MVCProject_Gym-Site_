namespace GymManagementDAL.Repositories.Interfaces;

public interface IMainRepository<T>
{
    T? GetById(int id);
    int Create(T entity);
    int Update(T entity);
    int Delete(int id);
    IEnumerable<T> GetAll();
}
