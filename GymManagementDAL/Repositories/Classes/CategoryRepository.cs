using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class CategoryRepository : ICategoryRepository
{
    private readonly GymContext _context;

    public CategoryRepository(GymContext context)
    {
        _context = context;
    }

    public int Create(Category entity)
    {
        _context.Categories.Add(entity);
        return _context.SaveChanges();
    }

    public int Delete(int id)
    {
        var category = _context.Categories.Find(id);
        if (category is null)
            return 0;

        _context.Categories.Remove(category);
        return _context.SaveChanges();
    }
    
    public int Update(Category entity)
    {
        _context.Categories.Update(entity);
        return _context.SaveChanges();
    }

    public IEnumerable<Category> GetAll() => _context.Categories.ToList();

    public Category? GetById(int id) => _context.Categories.Find(id);
}
