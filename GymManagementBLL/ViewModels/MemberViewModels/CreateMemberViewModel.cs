using System;
using System.ComponentModel.DataAnnotations;
using GymManagementDAL.Entities;
using GymManagementDAL.Entities.Enums;

namespace GymManagementBLL.ViewModels.MemberViewModels;

public class CreateMemberViewModel
{
    [Required(ErrorMessage = "Photo is required")]
    public string? Photo { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters and spaces")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    [RegularExpression(@"^[010|011|012|015][0-9]{8}$", ErrorMessage = "Phone number must be a valid Egyptian number")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Date of Birth is required")]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    public Gender Gender { get; set; }

    [Required(ErrorMessage = "Building Number is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Building Number must be a positive integer")]
    public int BuildingNumber { get; set; }

    [Required(ErrorMessage = "Street is required")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 30 characters")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Street must contain only letters, numbers, and spaces")]
    public string Street { get; set; } = null!;

    [Required(ErrorMessage = "City is required")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "City must be between 2 and 30 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City must contain only letters and spaces")]
    public string City { get; set; } = null!;

    public MemberHealthDetailsViewModel HealthRecordViewModel { get; set; } = null!;
}
