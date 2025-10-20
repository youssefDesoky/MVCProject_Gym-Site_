using System;
using GymManagementBLL.ViewModels.TrainerViewModels;

namespace GymManagementBLL.Services.Interfaces;

public interface ITrainerService
{
    IEnumerable<TrainerViewModel> GetAllTrainers();
    bool CreateTrainer(CreateTrainerViewModel model);
    TrainerDetailViewModel? GetTrainerDetails(int trainerId);
    bool UpdateTrainer(int trainerId, UpdateTrainerViewModel model);
    UpdateTrainerViewModel? GetTrainerToUpdate(int trainerId);
    bool RemoveTrainer(int trainerId);
}
