using System;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.LoginViewModels;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymManagementBLL.Services.Classes;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }


    public ApplicationUser? ValidateUser(LoginViewModel userInput)
    {
        var user = _userManager.FindByEmailAsync(userInput.Email).Result;

        var isValidPassword = _userManager.CheckPasswordAsync(user, userInput.Password).Result;

        return isValidPassword ? user : null;
    }
}
