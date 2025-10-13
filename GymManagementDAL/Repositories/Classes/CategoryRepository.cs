using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class CategoryRepository : MainCRUDRepository<Category>, ICategoryRepository
{
    public CategoryRepository(GymContext context) : base(context) {}
}
