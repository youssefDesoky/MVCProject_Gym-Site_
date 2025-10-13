using System;
using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes;

public class MainCRUDRepository<TEntity> : IMainCRUDRepository<TEntity> where TEntity : BaseEntity
{
    private readonly GymContext _context;

    public MainCRUDRepository(GymContext context)
    {
        _context = context;
    }

    public int Create(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        return _context.SaveChanges();
    }

    public int Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        return _context.SaveChanges();
    }

    public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
    {
        if (condition is not null)
            return _context.Set<TEntity>().AsNoTracking().Where(condition).ToList();

        return _context.Set<TEntity>().AsNoTracking().ToList();
    }

    public TEntity? GetById(int id) => _context.Set<TEntity>().Find(id);

    public int Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        return _context.SaveChanges();
    }
}
