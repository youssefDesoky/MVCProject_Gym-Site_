using System;
using System.ComponentModel.DataAnnotations;

namespace GymManagementBLL.ViewModels.MemberViewModels;

public class MemberHealthDetailsViewModel
{
    [Range(0.1, 500, ErrorMessage = "Weight must be between 0.1 and 500 kg.")]
    public decimal Weight { get; set; }

    [Range(1, 300, ErrorMessage = "Height must be between 1 and 300 cm.")]
    public decimal Height { get; set; }

    [Required(ErrorMessage = "Blood Type is required")]
    public string BloodType { get; set; } = null!;
    
    public string? Note { get; set; } = null!;
}
