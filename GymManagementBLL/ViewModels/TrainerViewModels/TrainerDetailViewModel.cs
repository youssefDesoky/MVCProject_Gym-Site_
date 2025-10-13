using System;

namespace GymManagementBLL.ViewModels.TrainerViewModels;

public class TrainerDetailViewModel : TrainerViewModel
{
    public string Gender { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string DateOfBirth { get; set; } = null!;
}
