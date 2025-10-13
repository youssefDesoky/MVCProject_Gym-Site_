using GymManagementBLL.Services.Interfaces;
using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes;

public class UnitOfWork : IUnitOfWork
{
    private readonly GymContext _context;
    private readonly Dictionary<string, object> repositories = new();

    public ISessionRepository SessionRepository { get; set; }

    public UnitOfWork(ISessionRepository sessionRepository, GymContext context)
    {
        SessionRepository = sessionRepository;
        _context = context;
    }


    public IMainCRUDRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        var entityName = typeof(TEntity).Name;

        if (repositories.TryGetValue(entityName, out var repository))
            return (IMainCRUDRepository<TEntity>)repository;

        var newRepository = new MainCRUDRepository<TEntity>(_context);
        
        repositories.Add(entityName, newRepository);

        return newRepository;
    }

    public int SaveChanges() => _context.SaveChanges();
}
