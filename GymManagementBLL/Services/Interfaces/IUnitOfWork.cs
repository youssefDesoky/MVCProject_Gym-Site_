using System;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Interfaces;

public interface IUnitOfWork
{
    IMainCRUDRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;

    ISessionRepository SessionRepository { get; set; }

    int SaveChanges();
}
