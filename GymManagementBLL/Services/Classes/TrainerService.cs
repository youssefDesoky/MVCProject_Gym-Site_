using System;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes;

public class TrainerService : ITrainerService
{
    private readonly IUnitOfWork _unitOfWork;

    public TrainerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool CreateTrainer(CreateTrainerModelView model)
    {
        try
        {
            if (IsEmailExist(model.Email)) 
                return false;
            if (IsPhoneNumberExist(model.Phone)) 
                return false;

            var trainer = new Trainer
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Address = new Address
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                },
                Specialties = model.Specialization
            };

            _unitOfWork.GetRepository<Trainer>().Create(trainer);
            _unitOfWork.SaveChanges();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<TrainerViewModel> GetAllTrainers()
    {
        var trainer = _unitOfWork.GetRepository<Trainer>().GetAll() ?? [];

        if (trainer is null || !trainer.Any())
            return [];

        return trainer.Select(x => new TrainerViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Phone = x.Phone,
            Email = x.Email,
            Specialization = x.Specialties.ToString()
        });
    }

    public TrainerDetailViewModel? GetTrainerDetails(int trainerId)
    {
        var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

        if (trainer is null)
            return null;

        return new TrainerDetailViewModel
        {
            Name = trainer.Name,
            Phone = trainer.Phone,
            Email = trainer.Email,
            Gender = trainer.Gender.ToString(),
            Address = FormatAddress(trainer.Address),
            DateOfBirth = trainer.DateOfBirth.ToShortDateString()
        };
    }

    public bool RemoveTrainer(int trainerId)
    {
        var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

        if (trainer is not null)
        {
            var sessions = _unitOfWork.GetRepository<Session>().GetAll().Where(s => s.TrainerId == trainerId).ToList();

            if (!sessions.Any())
            {
                _unitOfWork.GetRepository<Trainer>().Delete(trainer);
                _unitOfWork.SaveChanges();

                return true;
            }
        }
        
        return false;
    }

    public bool UpdateTrainer(int trainerId, UpdateTrainerViewModel model)
    {
        var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

        if (trainer is null)
            return false;
        
        trainer.Name = model.Name;
        trainer.Email = model.Email;
        trainer.Phone = model.Phone;
        trainer.DateOfBirth = model.DateOfBirth;
        trainer.Specialties = model.Specialization;
        trainer.UpdatedAt = DateTime.Now;

        _unitOfWork.GetRepository<Trainer>().Update(trainer);
        _unitOfWork.SaveChanges();

        return true;
    }

    #region Helper Methods
    private string FormatAddress(Address address)
    {
        if (address is null)
            return "N/A";

        return $"{address.BuildingNumber}, {address.Street}, {address.City}";
    }

    private bool IsEmailExist(string email) => _unitOfWork.GetRepository<Trainer>().GetAll().Any(x => x.Email == email);

    private bool IsPhoneNumberExist(string phone) => _unitOfWork.GetRepository<Trainer>().GetAll().Any(x => x.Phone == phone);

    #endregion
}
