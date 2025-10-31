using System;
using GymManagementBLL.ViewModels.LoginViewModels;
using GymManagementDAL.Entities;

namespace GymManagementBLL.Services.Interfaces;

public interface IAccountService
{
    ApplicationUser? ValidateUser(LoginViewModel userInput);
}
