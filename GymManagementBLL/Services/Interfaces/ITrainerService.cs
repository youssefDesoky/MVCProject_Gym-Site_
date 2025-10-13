using System;
using GymManagementBLL.ViewModels.TrainerViewModels;

namespace GymManagementBLL.Services.Interfaces;

public interface ITrainerService
{
    IEnumerable<TrainerViewModel> GetAllTrainers();
    bool CreateTrainer(CreateTrainerModelView model);
    TrainerDetailViewModel? GetTrainerDetails(int trainerId);
    bool UpdateTrainer(int trainerId, UpdateTrainerViewModel model);
    bool RemoveTrainer(int trainerId);
}
